using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAI : Unit
{
	public GameObject floatingTextPrefab;
	public float movespeed;
	public float damage = 1;
	private Vector2 movement;
	public Transform spawner;
	public bool spawned, dead;
	public GameObject[] itemdrops;
	public int dropamt;
	private AudioSource audio;
	public GameObject maskvfx;

	LevelManager theLevelManager;

	Animator myAnim;
	private Rigidbody2D rb;
	private Player thePlayer;


	public float attackRange = 2f;
	public float attackRate = 1f;
	float nextAttackTime = 0f;
	public bool isAttack = false;

    public float jumpRange = 10f;
    public float jumpRate = 3f;
    float nextJumpTime = 0f;

    private float timer;
	public float nextCastTime;
	public bool isCast = false;
	public float nextCast = 0.2f;

	public float space = 5f;

	public LayerMask attackMask;
	public int damageToGive;
	public Transform attackPoint;

    public GameObject bullet1;
    public Transform bulletPos;

    public bool isJump = false;
    //public bool isHurt = false;
    public bool isDead = false;



	void Start()
	{
		myAnim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<Player>();
		theLevelManager = FindObjectOfType<LevelManager>();
		hitpoints = maxhitpoints;
		sr = GetComponent<SpriteRenderer>();
		originalColor = sr.color;
		audio = GetComponent<AudioSource>();
		//transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
		dead = false;
		canMove = true;
	}
		void Update()
	{
        myAnim.SetBool("Jump", isJump);
        myAnim.SetBool("Attacking", isAttack);

		if (isAttack) return;
		//if (isHurt) return;
		if (isCast) return;
		//if (isHurt) return;

		timer += Time.deltaTime;

		myAnim.SetBool("Death", dead);
		if (hitpoints <= 0)
		{
			StartCoroutine(Death());
		}

		if (dead) // Stop movement if the enemy is dead
		{
			movement = Vector2.zero;
		}
		else
		{
			Vector3 direction = thePlayer.transform.position - transform.position;
			direction.Normalize();
			if (!thePlayer.gameObject.activeSelf && spawned)
			{
				transform.position = spawner.position;
			}
			else
			{
				movement = direction;
			}
			SetHealth(hitpoints, maxhitpoints);

			// Flip the enemy sprite based on the movement direction
			if (movement.x < 0) // If moving left
			{
				transform.localScale = new Vector3(-1, 1, 1); // Flip the sprite
			}
			else if (movement.x > 0) // If moving right
			{
				transform.localScale = new Vector3(1, 1, 1); // Reset the sprite scale
			}
		}
		if (knockbackcounter > 0)
		{
			knockbackcounter -= Time.deltaTime;
			movement = knockbackdir;
			movespeed = knockbackforce;
		}
        if (timer > nextCastTime)
        {

            timer = 0;
            myAnim.SetTrigger("CastStorm");
            isCast = true;
        }
        else if (Vector2.Distance(thePlayer.transform.position, rb.position) >= jumpRange && Vector2.Distance(thePlayer.transform.position, rb.position) > 1 && Time.time > nextJumpTime)
        {
            print("Jump");
            myAnim.SetTrigger("JumpAttack");
            isJump = true;

            StartCoroutine("Jump");
        }
        else if (Vector2.Distance(thePlayer.transform.position, rb.position) <= attackRange && Time.time >= nextAttackTime)
        {
            myAnim.SetTrigger("Attack");
            isAttack = true;

            StartCoroutine("Attacking");
        }
        else
        {
            return;
        }
    }

	void FixedUpdate()
	{
		if (canMove) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
	}
	private void Move(Vector2 direction)
	{
		rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
	}
	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<SlowingProjectile>() && !dead && collision.tag != "Enemy")
		{
			movespeed *= collision.GetComponent<SlowingProjectile>().slowfactor;
			TakeHit(collision.GetComponent<SlowingProjectile>().damage);
			Knockback();
			audio.PlayOneShot(hitsound);
			StartCoroutine(DamageFeedback());
			if (floatingTextPrefab)
			{
				var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
				floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
			}
		}
		else if (collision.GetComponent<ExplodingProjectile>() && !dead && collision.tag != "Enemy")
		{
			TakeHit(collision.GetComponent<ExplodingProjectile>().damage);
			Knockback();
			audio.PlayOneShot(hitsound);
			StartCoroutine(DamageFeedback());
			if (floatingTextPrefab)
			{
				var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
				floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
			}
		}
		else if (collision.GetComponent<ProjectileScript>() && !dead && collision.tag != "Enemy")
		{

			TakeHit(collision.GetComponent<ProjectileScript>().damage);
			Knockback();
			audio.PlayOneShot(hitsound);
			StartCoroutine(DamageFeedback());
			if (floatingTextPrefab)
			{
				var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
				floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
			}
		}
	}
	public IEnumerator Death()
	{
		rb.velocity = Vector2.zero;
		if (!dead)
		{
			dead = true;
			Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
			Destroy(gameObject);
		}
		healthbar.gameObject.SetActive(false);
		yield return new WaitForSeconds(myAnim.GetCurrentAnimatorStateInfo(0).length + myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
		gameObject.SetActive(false);
		if (spawned) theLevelManager.enemieskilled += 1; theLevelManager.totalenemieskilled += 1;
		for (int i = 0; i < Random.Range(1, dropamt + 1); i++)
		{
			Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
		}
	}
	protected void Knockback()
	{
		knockbackcounter = knockbacklength;
	}

	public void BossAttack()
	{

		if (thePlayer._isDashing == true)
		{
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(), thePlayer.GetComponent<Collider2D>());
		}
		else
		{
			Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask); // Detect player in range of attack

			if(hitPlayer)
			{
				thePlayer.TakeHit(damageToGive);
			}
		}
		nextAttackTime = Time.time + attackRate;

	}
	public IEnumerator Attacking()
	{
		yield return new WaitForSeconds(0.2f);
		isAttack = false;
	}

    public IEnumerator Jump()
    {
        //WaitForEndOfFrame w = new WaitForEndOfFrame();
        //float t = 0;
        //Vector2 dir = (thePlayer.transform.position - transform.position).normalized;
        //while(t < 0.2f)
        //      {
        //          transform.position += (Vector3)dir * 10 * Time.deltaTime;
        //          t += Time.deltaTime;
        //          yield return w;
        //      }
        yield return new WaitForSeconds(0.2f);
        isJump = false;
    }


    private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}


    public void BossJump()
    {

        if (nextJumpTime > Time.time) return;


        if (transform.position.x < thePlayer.transform.position.x)
        {
            rb.position = new Vector2(thePlayer.transform.position.x - space, thePlayer.transform.position.y);
        }
        if (transform.position.x > thePlayer.transform.position.x)
        {
            rb.position = new Vector2(thePlayer.transform.position.x + space, thePlayer.transform.position.y);
        }

        nextJumpTime = Time.time + jumpRate;
        return;
    }


    public IEnumerator BossCast()
    {

        isCast = true;

        Instantiate(bullet1, bulletPos.position, Quaternion.identity);
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 45));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 90));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 135));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 180));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 225));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 270));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 315));

        yield return new WaitForSeconds(nextCast);

        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 22));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 67));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 112));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 157));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 202));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 247));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 292));
        Instantiate(bullet1, bulletPos.position, Quaternion.Euler(0, 0, 337));


    }
    public void AfterCast()
	{
		isCast = false;
	}


	//public void BossHurt()
	//{
	//	isHurt = true;
	//	isCast = false;
	//	isAttack = false;
	//	isJump = false;
	//}

	//public void BossBack()
	//{
	//	isHurt = false;
	//}
}
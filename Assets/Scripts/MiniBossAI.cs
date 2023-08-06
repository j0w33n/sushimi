using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAI : Enemy
{
	//Attack
	public float attackRange = 2f;
	public bool isAttack = false;
	float nextattacktime;
	public float attackrate;
    //Charge
    public float chargerange = 10f;
    public float chargespeed = 1f;
    public float chargerate = 3f;
    float nextchargetime = 0f;
	public bool isCharging = false;
	// Smash
    public GameObject dustwave;
    public Transform dustPos;
	float nextsmashtime;
	public float smashrate;
	public float smashrange;

	public GameObject doublebarrelgun;
	public AudioClip attacksound,smashsound,chargesound,deathsound,dustsound;
	//public GameObject dust;
	Coroutine charge;
	protected override void Start() {
		gameObject.SetActive(false);
		isCharging = false;
		isAttack = false;
		base.Start();
    }
    protected override void Update()
	{
		anim.SetBool("Charge", isCharging);
        anim.SetBool("Attacking", isAttack);
		anim.SetBool("Death", dead);
		if (!canMove) return;
		float dist = (transform.position - player.transform.position).magnitude;
        
        if (dist <= chargerange && dist > smashrange && Time.time >= nextchargetime)
        {
			charge = StartCoroutine(BossCharge());
		}
		if (charge == null && dist <= smashrange && dist > attackRange && Time.time >= nextsmashtime) {
			BossSmash();
		}
		if (dist <= attackRange && Time.time >= nextattacktime) {
            isAttack = true;
        } 
		else {
            isAttack = false;
        }
        base.Update();
    }
	protected override void FixedUpdate()
	{
		if (canMove) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
	}
	private void Move(Vector2 direction)
	{
		rb.MovePosition((Vector2)transform.position + (movespeed * Time.deltaTime * direction));
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>() && anim.GetCurrentAnimatorStateInfo(0).IsName("MiniBossCharge")) {
			collision.gameObject.GetComponent<Player>().knockbackforce = 12;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>() && !anim.GetCurrentAnimatorStateInfo(0).IsName("MiniBossCharge")) {
            collision.gameObject.GetComponent<Player>().knockbackforce = 3;
        }
    }
    private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.DrawWireSphere(transform.position, chargerange);
		Gizmos.DrawWireSphere(transform.position, smashrange);
	}
    IEnumerator BossCharge() 
    {
		Vector2 dir = (player.transform.position - transform.position).normalized;
        if (Time.time < nextchargetime) yield return null;
		isCharging = true;
		movement = Vector2.zero;
		yield return new WaitForSeconds(0.5f);
		movement = chargespeed * dir;
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - .2f);
		isCharging = false;
		//Destroy(Instantiate(dust, transform.position, transform.rotation), 1f);
		nextchargetime = Time.time + chargerate;
		charge = null;
	}
    public void BossAttack() {
		attackrate = anim.GetCurrentAnimatorStateInfo(0).length;
		nextattacktime = Time.time + attackrate;
		AudioManager.instance.PlaySFX(attacksound);
    }
    void BossSmash()
    {
		movement = Vector2.zero;
		anim.SetTrigger("Smash");
		Instantiate(dustwave, dustPos.position, Quaternion.identity);
		nextsmashtime = Time.time + smashrate;
	}
    public void PlayDeathSound() {
		AudioManager.instance.PlaySFX(deathsound);
    }
	public void PlaySmashSound() {
		AudioManager.instance.PlaySFX(smashsound);
	}
	public void PlayChargeSound() {
		AudioManager.instance.PlaySFX(chargesound);
	}
	public override IEnumerator Death()
	{
		canMove = false;
		if (!dead)
		{
			dead = true;
			Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
		}
		healthbar.gameObject.SetActive(false);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		//Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
		for (int i = 0; i < Random.Range(1, dropamt + 1); i++)
		{
			Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
		}
		Instantiate(doublebarrelgun, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
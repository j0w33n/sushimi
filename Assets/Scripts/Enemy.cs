using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public GameObject floatingTextPrefab;
    public float movespeed;
    public float damage = 1;
    protected Player player;
    protected Rigidbody2D rb;
    protected Vector2 movement;
    protected LevelManager levelManager;
    public Transform spawner;
    public bool spawned,dead;
    public GameObject[] itemdrops;
    public int dropamt;
    public AudioSource audio;
    public Animator anim;
    public GameObject maskvfx;
    //public Transform indicator;
    //Renderer rd;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        hitpoints = maxhitpoints;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        audio = GetComponent<AudioSource>();
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        dead = false;
        canMove = false;
        //rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        anim.SetBool("Death", dead);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        if (dead) // Stop movement if the enemy is dead
        {
            movement = Vector2.zero;
        } else {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            if (!player.gameObject.activeSelf && spawned) {
                transform.position = spawner.position;
            } 
            else {
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
        if(knockbackcounter > 0) {
            knockbackcounter -= Time.deltaTime;
            movement = knockbackdir;
            movespeed = knockbackforce;
        }
        /*if (!rd.isVisible) {
            if (!indicator.gameObject.activeSelf) indicator.gameObject.SetActive(true);
            var dir = player.transform.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir);
            if (ray.collider.gameObject.layer == 3) indicator.transform.position = ray.point;
        } else {
            if (indicator.gameObject.activeSelf) indicator.gameObject.SetActive(false);
        }*/
    }

    protected virtual void FixedUpdate() {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Spawn")) canMove = true;
        if (canMove) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
    }
    private void Move(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SlowingProjectile>() && !dead && collision.tag != "Enemy")
        {
            movespeed *= collision.GetComponent<SlowingProjectile>().slowfactor;
            TakeHit(collision.GetComponent<SlowingProjectile>().damage);
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab)
            {
                var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
            }
        }
        else if (collision.GetComponent<ExplodingProjectile>() && !dead && collision.tag != "Enemy") {
            TakeHit(Mathf.Abs(collision.GetComponent<ExplodingProjectile>().damage));
            print(collision.GetComponent<ExplodingProjectile>().damage);
            Knockback();
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab) {
                var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
            }
        }
        else if (collision.GetComponent<ProjectileScript>() && !dead && collision.tag != "Enemy") {
            
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
            Knockback();
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab) {
                var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
            }
        }
    }
    public virtual IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        if (!dead) {
            Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
            dead = true;
        }
        Destroy(healthbar.gameObject);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
        Destroy(gameObject);
        if (spawned) levelManager.enemieskilled += 1; levelManager.totalenemieskilled += 1;
        for(int i = 0; i < Random.Range(1, dropamt + 1); i++) {
            Instantiate(itemdrops[Random.Range(0, itemdrops.Length)],transform.position,transform.rotation);
        }
    }
    protected void Knockback() {
        knockbackcounter = knockbacklength;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public GameObject floatingTextPrefab;
    public float movespeed;
    public float damage = 1;
    private Player player;
    private Rigidbody2D rb;
    private Vector2 movement;
    LevelManager levelManager;
    public Transform spawner;
    public bool spawned,dead;
    public GameObject[] itemdrops;
    public int dropamt;
    private AudioSource audio;
    Animator anim;
    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Death", dead);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        Vector3 direction = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        if (!player.gameObject.activeSelf && spawned) {
            transform.position = spawner.position;
        } 
        else {
            movement = direction;
        }
        SetHealth(hitpoints, maxhitpoints);
    }
    private void FixedUpdate() {
        if (canMove) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
    }
    private void Move(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SlowingProjectile>() && !dead)
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
        else if (collision.GetComponent<ExplodingProjectile>() && !dead) {
            TakeHit(collision.GetComponent<ExplodingProjectile>().damage);
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab) {
                var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
            }
        }
        else if (collision.GetComponent<ProjectileScript>() && !dead) {
            
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab) {
                var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
            }
        }
    }
    public IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        dead = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        gameObject.SetActive(false);
        healthbar.gameObject.SetActive(false);
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
        if (spawned) levelManager.enemieskilled += 1; levelManager.totalenemieskilled += 1;
        for(int i = 0; i < Random.Range(1, dropamt + 1); i++) {
            Instantiate(itemdrops[Random.Range(0, itemdrops.Length)],transform.position,transform.rotation);
        }
    }
}

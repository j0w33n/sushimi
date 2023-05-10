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
    public bool canMove;
    public bool spawned;
    public GameObject[] itemdrops;
    public int dropamt;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        hitpoints = maxhitpoints;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        gameObject.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
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
        if (canMove) Move(movement);
    }
    private void Move(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ProjectileScript>())
        {
            //Destroy(collision.gameObject);
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
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
        yield return new WaitForSeconds(.25f);
        gameObject.SetActive(false);
        if(spawned) levelManager.enemieskilled += 1; levelManager.totalenemieskilled += 1;
        for(int i = 0; i < Random.Range(1, dropamt + 1); i++) {
            Instantiate(itemdrops[Random.Range(0, itemdrops.Length)],transform.position,transform.rotation);
        }
    }
}

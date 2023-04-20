using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GeneralFunctions
{
    public float movespeed;
    private Player player;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        rb.velocity = new Vector2(movespeed, rb.velocity.y) * (player.transform.position - transform.position).normalized;
        hitpoints = maxhitpoints;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
       // SetHealth(hitpoints, maxhitpoints);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<ProjectileScript>() ) {
            //Destroy(collision.gameObject);
            TakeHit(1);
            StartCoroutine(DamageFeedback());
        }
    }
   
    public IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.25f);
       gameObject.SetActive(false);
    }
}

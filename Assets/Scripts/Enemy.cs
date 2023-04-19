using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<ProjectileScript>() && collision.tag != "Enemy") {
            Destroy(gameObject);
        }
    }
}

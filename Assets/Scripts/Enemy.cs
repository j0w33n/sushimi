using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movespeed;
    private Player player;
    private Rigidbody2D rb;

    public int maxHealth = 3;
    public Color damageFeedbackColor = new Color(1, 0, 0);
    Color originalColor;
    SpriteRenderer sr;
    int currentHealth;
    private SpriteRenderer sprite;
    private Color originalColors;
    public Color damageFeedbackColors;
    EnemyBehaviour health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        rb.velocity = new Vector2(movespeed, rb.velocity.y) * (player.transform.position - transform.position).normalized;

        health = GetComponent<EnemyBehaviour>();
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                health.TakeHit(damage);
            }
            else
            {
                //Instantiate(deathSplosion, gameObject.transform.position, Quaternion.identity); ; // Create object
                Destroy(gameObject);
            }
        }
    

    }

    IEnumerator DamageFeedback()
    {
        sr.color = damageFeedbackColor;
        yield return new WaitForSeconds(0.2f);
        sr.color = originalColor;
    }


    public IEnumerator dead()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}

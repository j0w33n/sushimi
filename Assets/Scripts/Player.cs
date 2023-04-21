using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GeneralFunctions
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    [Header("Knockback")]
    public float knockbackforce;
    public float knockbackLength; // Amount of time the player being knocked back
    private float knockbackcounter; // Count down of time for players being knocked back
    public bool knockFromRight;
    private AudioSource audio;
    private Animator anim;
    [Header("Dash")]
    public float immunityDuration = 0.3f;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;

    void Start() {
        hitpoints = maxhitpoints;
        originalColor = sr.color;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        activeMoveSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Horizontal", movement.x);
        SetHealth(hitpoints, maxhitpoints);
        /*if (knockbackCounter > 0)
        {
            StartCoroutine(Invulnerability());
            knockbackCounter -= Time.deltaTime;
            if (knockFromRight == true)
            { // Kockback based on where the player is hit from.
                rb.velocity = new Vector3(-knockbackForce, 0.5f, 0.0f); //The force to push the player back
            }
            if (knockFromRight == false)
            {
                rb.velocity = new Vector3(knockbackForce, 0.5f, 0.0f); // The force to push the player back
            }
        }*/
        if (knockbackcounter > 0) {
            knockbackcounter -= Time.deltaTime; // count down time
            if (transform.localRotation.y == 0) {
                rb.velocity = new Vector3(-knockbackforce, knockbackforce, 0.0f); // push player back
            } else {
                rb.velocity = new Vector3(knockbackforce, knockbackforce, 0.0f);
            }
        }
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashCoolCounter <=0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }
            if (dashCoolCounter > 0)
            {
                dashCoolCounter = Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Enemy>()) {
            TakeHit(1);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
            //Knockback();
        }
        if((transform.position - collision.transform.position).magnitude < 0) {
            knockFromRight = true;
        } 
        else {
            knockFromRight = false;
        }
    }
    public void Knockback()
    {
        knockbackcounter = knockbackLength;
    }

    // For player's immunity
    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(immunityDuration);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * activeMoveSpeed * Time.fixedDeltaTime);
    }
    public IEnumerator Death() {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.25f);
        gameObject.SetActive(false);
    }
}

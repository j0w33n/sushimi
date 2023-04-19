using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackLength; // Amount of time the player being knocked back
    private float knockbackCounter; // Count down of time for players being knocked back
    public bool knockFromRight;
    public AudioSource knockbackSound;

    [Header("Dash")]
    public float immunityDuration = 0.3f;

    void Start() {
        
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (knockbackCounter > 0)
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
        }
    }
    public void Knockback()
    {
        knockbackCounter = knockbackLength;
    }

    // For player's immunity
    public IEnumerator Invulnerability()
    {


        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(immunityDuration);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public float moveSpeed = 5f,rotationspeed = 999f;
    public Rigidbody2D rb;
    private Vector2 movement;
    [Header("Knockback")]
    public float knockbackforce;
    public float knockbackLength; // Amount of time the player being knocked back
    private float knockbackcounter; // Count down of time for players being knocked back
    public bool knockFromRight;
    private AudioSource audio;
    public Animator anim;
    [Header("Dash")]
    [SerializeField] private float _dashingVelocity = 20f; //dash speed
    [SerializeField] private float _dashingTime = 1f; //dash time
    private Vector2 _dashingDir; //dash direction
    private bool _isDashing;
    private bool _canDash = true;
    private float nextdashtime;
    public float immunityDuration = 0.3f;
    private LevelManager levelManager;
    public Vector3 respawnpoint;
    public bool dead;
    /*public float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;*/
    void Start() {
        hitpoints = maxhitpoints;
        originalColor = sr.color;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        // activeMoveSpeed = moveSpeed;
        respawnpoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Rotation", transform.localRotation.z);
        anim.SetBool("Horizontal", transform.localRotation.y == 0 || transform.localRotation.y == 180);
        anim.SetFloat("Speed", movement.magnitude);

        //anim.SetBool("IsDashing", _isDashing);
        SetHealth(hitpoints, maxhitpoints);
        if(movement.magnitude != 0) {
            if (movement.y == 0) {
                if (movement.x > 0) {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                } else {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
            if (movement.x == 0) {
                if(movement.y > 0) {
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                } else {
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                }
            }
            if(movement.x > 0) {
                if (movement.y > 0) {
                    transform.localRotation = Quaternion.Euler(0, 0, 45);
                } else if (movement.y < 0){
                    transform.localRotation = Quaternion.Euler(0, 0, -45);
                }
            }
            if (movement.x < 0) {
                if (movement.y > 0) {
                    transform.localRotation = Quaternion.Euler(0, 0, 135);
                } else if (movement.y < 0) {
                    transform.localRotation = Quaternion.Euler(0, 0, -135);
                }
            }
        } 
        else {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
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
        //transform.Rotate(0, movement.magnitude * moveSpeed * Time.deltaTime, 0);
        /*if (knockbackcounter > 0) {
            knockbackcounter -= Time.deltaTime; // count down time
            if(transform.localRotation.y == 0) {
                rb.velocity = new Vector3(-knockbackforce, 0, knockbackforce);
            }
        }*/
        if (hitpoints <= 0) {
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
        }
        if (Input.GetButtonDown("Dash") && _canDash) {
            _isDashing = true;
            //audio.PlayOneShot(dashSound);
            _canDash = false;
            _dashingDir = movement.normalized;
        }
        if (_isDashing) {
            //Vector3 dashPosition = transform.position + (Vector3)_dashingDir * _dashingVelocity;
            //rb.MovePosition(transform.position + (Vector3)movement.normalized * _dashingVelocity);
            movement = _dashingDir.normalized * _dashingVelocity;
            StartCoroutine(Invulnerability());
            return;
        }

        if (/*Mathf.Abs(movement.magnitude) > 0 &&*/ Time.time >= nextdashtime) { // checks if player is moving
            _canDash = true;
            nextdashtime = Time.time + _dashingTime;
        }
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //rb.velocity = movement.normalized * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Enemy>() && !dead) {
            TakeHit(1);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
            Knockback();
        }
        /*if((transform.position - collision.transform.position).magnitude < 0) {
            knockFromRight = true;
        } 
        else {
            knockFromRight = false;
        }*/
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
        if (_isDashing) _isDashing = false;
    }
    public void Death() {
        rb.velocity = Vector2.zero;
    }
}

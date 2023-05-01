using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    [Header("Knockback")]
    //public float knockbackforce;
    //public float knockbackLength; // Amount of time the player being knocked back
    //private float knockbackcounter; // Count down of time for players being knocked back
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
        anim.SetBool("Horizontal", transform.localRotation.y == 0 || Mathf.Abs(transform.localRotation.y) == 180);
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
        if (hitpoints <= 0) {
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
            transform.localRotation = Quaternion.identity;
        }
        if (Input.GetButtonDown("Dash") && _canDash) {
            _isDashing = true;
            //audio.PlayOneShot(dashSound);
            _canDash = false;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleRight")) {
                _dashingDir = (movement + new Vector2(0.1f, 0f)).normalized;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleDown")) {
                _dashingDir = (movement + new Vector2(0f, -0.1f)).normalized;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleUp")) {
                _dashingDir = (movement + new Vector2(0f, 0.1f)).normalized;
            } 
            else {
                _dashingDir = movement.normalized;
            }
            /*if (transform.localRotation == Quaternion.identity) {
                _dashingDir = new Vector2(1, transform.localRotation.z).normalized;
            }
            else {
                _dashingDir = new Vector2(transform.localRotation.y, transform.localRotation.z).normalized;
            }*/
            
        }
        if (_isDashing) {
            //Vector3 dashPosition = transform.position + (Vector3)_dashingDir * _dashingVelocity;
            //rb.MovePosition(transform.position + (Vector3)movement.normalized * _dashingVelocity);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleRight") && transform.localRotation.y < 0) {
                _dashingDir = (movement + new Vector2(-0.1f, 0f)).normalized;
                movement = _dashingDir * _dashingVelocity;
            } 
            else {
                movement = _dashingDir.normalized * _dashingVelocity;
            }
            //movement = _dashingDir * _dashingVelocity;
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
            TakeHit(collision.gameObject.GetComponent<Enemy>().damage);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Trap>()) {
            TakeHit(collision.GetComponent<Trap>().damage);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
    }
    // For player's immunity
    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        yield return new WaitForSeconds(immunityDuration);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        if (_isDashing) _isDashing = false;
    }
    public void Death() {
        rb.velocity = Vector2.zero;
    }
}

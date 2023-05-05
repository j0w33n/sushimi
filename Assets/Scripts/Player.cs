using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class Player : Unit
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    private AudioSource audio;
    public Animator anim;
    [Header("Dash")]
    [SerializeField] private float _dashingVelocity = 20f; //dash speed
    public float _dashingTime = 1f; //dash time
    private Vector2 _dashingDir; //dash direction
    private bool _isDashing;
    private bool _canDash = true;
    private float nextdashtime;
    public float immunityDuration = 0.3f;
    private LevelManager levelManager;
    public Vector3 respawnpoint;
    public bool dead;
    public bool canMove;
    void Start() {
        hitpoints = maxhitpoints;
        originalColor = sr.color;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        respawnpoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            //movement.x = VirtualJoystick.GetAxis("Horizontal", 0);
            //movement.y = VirtualJoystick.GetAxis("Vertical", 0);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        anim.SetFloat("Horizontal",Mathf.Sign(movement.x));
        anim.SetFloat("Vertical",Mathf.Sign(movement.y));
        anim.SetFloat("Speed", movement.magnitude);

        //anim.SetBool("IsDashing", _isDashing);
        SetHealth(hitpoints, maxhitpoints);
        if (hitpoints <= 0) {
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
            transform.localRotation = Quaternion.identity;
        }
        Dash();
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
    public void Dash() {
        if (Input.GetButtonDown("Dash") &&_canDash) {
            _isDashing = true;
            //audio.PlayOneShot(dashSound);
            _canDash = false;
            _dashingDir = movement.normalized;
        }
        if (_isDashing) {
            movement = _dashingDir.normalized * _dashingVelocity;
            StartCoroutine(Invulnerability());
            return;
        }

        if (/*Mathf.Abs(movement.magnitude) > 0 &&*/ Time.time >= nextdashtime) { // checks if player is moving
            _canDash = true;
            nextdashtime = Time.time + _dashingTime;
        }
    }
}

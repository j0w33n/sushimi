using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    //public float moveSpeed = 5f;
    public Rigidbody2D rb;
    //private Vector2 movement;

    private Vector3 moveDir;
    private const float moveSpeed = 60f;
    float moveX = 0f;
    float moveY = 0f;
    [Header("Knockback")]
    public float knockbackforce;
    public float knockbackLength; // Amount of time the player being knocked back
    private float knockbackcounter; // Count down of time for players being knocked back
    public bool knockFromRight;
    private AudioSource audio;
    public Animator anim;
    //[Header("Dash")]
    //[SerializeField] private float _dashingVelocity = 20f; //dash speed
    //[SerializeField] private float _dashingTime = 1f; //dash time
    //private Vector2 _dashingDir; //dash direction
    //private bool _isDashing;
    //private bool _canDash = true;
    //private float nextdashtime;
    public float immunityDuration = 0.3f;
    private LevelManager levelManager;
    public Vector3 respawnpoint;
    public bool dead;
    /*public float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;*/
    public bool isDashButtonDown;
    [SerializeField] private LayerMask dashLayerMask;

    void Start() {
        hitpoints = maxhitpoints;
        originalColor = sr.color;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        // activeMoveSpeed = moveSpeed;
        respawnpoint = transform.position;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //float moveX = 0f;
        //float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }
        //moveDir = new Vector3(MoveX, MoveY).normalized;

        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        //anim.SetFloat("Horizontal",movement.x);
        //anim.SetFloat("Vertical", movement.y);
        //anim.SetFloat("Speed", movement.magnitude);

        //anim.SetBool("IsDashing", _isDashing);
        SetHealth(hitpoints, maxhitpoints);
        //float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
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
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
        }
        //if (Input.GetButtonDown("Dash") && _canDash)
        //{
        //    _isDashing = true;
        //    //audio.PlayOneShot(dashSound);
        //    _canDash = false;
        //    _dashingDir = movement.normalized;
        //}
        //if (_isDashing) {
        //    movement = _dashingDir.normalized * _dashingVelocity;
        //    StartCoroutine(Invulnerability());
        //    return;
        //}

        //if (/*Mathf.Abs(movement.magnitude) > 0 &&*/ Time.time >= nextdashtime) { // checks if player is moving
        //    _canDash = true;
        //    nextdashtime = Time.time + _dashingTime;
        //}
        //if (Input.GetButtonDown("Dash"))
        //{
        //    isDashButtonDown = true;
        //}
    }
    private void FixedUpdate() {
        rb.velocity = moveDir * moveSpeed;
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if(isDashButtonDown)
        {
            float dashAmount = 50f;
            Vector3 dashPosition = transform.position + moveDir * dashAmount;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask); //dashLayerMask so cant dash through walls
            if(raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;
            }
            rb.MovePosition(transform.position + moveDir * dashAmount);
            isDashButtonDown = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Enemy>() && !dead) {
            TakeHit(1);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
            Knockback();
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
        //if (_isDashing) _isDashing = false;
    }
    public void Death() {
        rb.velocity = Vector2.zero;
    }
}

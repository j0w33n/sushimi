using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    public bool mobileControls;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    private AudioSource audio;
    public AudioClip dashsound;
    public Animator anim;
    [Header("Dash")]
    [SerializeField] private float _dashingVelocity = 20f; //dash speed
    public float _dashingTime = 1f; //dash time
    private Vector2 _dashingDir; //dash direction
    public bool _isDashing;
    private bool _canDash = true;
    public float nextdashtime;
    public float immunityDuration = 0.3f;
    private LevelManager levelManager;
    public Vector3 respawnpoint;
    public bool dead;
    public int currentweapon;
    public List<Weapon> weapons;
    //public Transform arrow;
    //[SerializeField]Transform target;
    void Start() {
        hitpoints = PlayerPrefs.GetFloat("Max Health", 5);
        originalColor = sr.color;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        respawnpoint = transform.position;
        canMove = true;
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        if(SceneManager.GetActiveScene().name == "Tutorial")SwitchWeapon(0);
        //arrow.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove && !_isDashing) {
            if (mobileControls) {
                movement.x = VirtualJoystick.GetAxis("Horizontal", 0);
                movement.y = VirtualJoystick.GetAxis("Vertical", 0);
            } 
            else {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }
            
        }
        anim.SetFloat("Horizontal",movement.x);
        anim.SetFloat("Vertical",movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        //anim.SetBool("IsDashing", _isDashing);
        SetHealth(hitpoints, maxhitpoints);
        if (hitpoints <= 0) {
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
        }
        if (knockbackcounter > 0) {
            knockbackcounter -= Time.deltaTime;
            movement = knockbackdir.normalized * knockbackforce;
        }
        /*FindClosestRoom();
        var dir = target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        print(collision.gameObject.name);
        if (collision.gameObject.GetComponent<Enemy>() && !dead && !collision.gameObject.GetComponent<Enemy>().dead) {
            TakeHit(collision.gameObject.GetComponent<Enemy>().damage);
            knockbackdir = transform.position - collision.transform.position;
            Knockback();
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Trap>() && !dead) {
            //TakeHit(collision.GetComponent<Trap>().damage);
            knockbackdir = transform.position - collision.transform.position;
            Knockback();
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
        if (collision.GetComponent<HealthCollectible>() && hitpoints != maxhitpoints) {
            hitpoints += collision.GetComponent<HealthCollectible>().value;
            AudioManager.instance.PlaySFX(AudioManager.instance.healthSound);
            Destroy(collision.gameObject);
            //collision.GetComponent<Collectible>().hasTarget = false;
        }
        else if (collision.GetComponent<Part>()) {
            levelManager.parts += collision.GetComponent<Part>().value;
            AudioManager.instance.PlaySFX(AudioManager.instance.partSound);
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<EnemyProjectile>() && !dead) {
            TakeHit(collision.gameObject.GetComponent<EnemyProjectile>().damage);
            knockbackdir = transform.position - collision.transform.position;
            Knockback();
            audio.PlayOneShot(hitsound);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
    }
    // For player's immunity
    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 10, true);
        yield return new WaitForSeconds(immunityDuration);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
        if (_isDashing) _isDashing = false;
    }
    public void Death() {
        dead = true;
        rb.velocity = Vector2.zero;
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
        //audio.PlayOneShot(deathsound);
    }
    public void Dash() {
        if (_canDash) {
            _isDashing = true;
            audio.PlayOneShot(dashsound);
            _canDash = false;
            _dashingDir = movement.normalized;
        }
        if (_isDashing) {
            movement = _dashingDir.normalized * _dashingVelocity;
            StartCoroutine(Invulnerability());
            return;
        }

        if (!dead && Time.time >= nextdashtime) { // checks if player is moving
            _canDash = true;
            nextdashtime = Time.time + _dashingTime;
        }
    }
    public void SwitchWeapon(int weaponnum) {
        currentweapon = weaponnum;
        foreach(var i in weapons) {
            i.gameObject.SetActive(false);
            weapons[currentweapon].gameObject.SetActive(true);
        }
    }
    void Knockback() {
        knockbackcounter = knockbacklength;
    }
    /*void FindClosestRoom() {
        float closest = 999;
        for(int i = 1;i < levelManager.rooms.Count;i++) {
            var dist = (levelManager.rooms[i].transform.position - transform.position).magnitude;
            if(dist < closest) {
                closest = dist;
            }
            if ((levelManager.rooms[i].transform.position - transform.position).magnitude == closest && levelManager.rooms[i].gameObject.activeSelf) {
                target = levelManager.rooms[i].transform;
            }
        }
    }*/
}

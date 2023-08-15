using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    public bool mobileControls;
    public float moveSpeed = 5f;
    public float ogmovespeed;
    public Rigidbody2D rb;
    public Vector2 movement;
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
    public int absmaxhealth;
    void Start() {
        hitpoints = PlayerPrefs.GetFloat("Max Health", 5);
        originalColor = sr.color;
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        respawnpoint = transform.position;
        canMove = true;
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        moveSpeed = ogmovespeed;
        if (FindObjectOfType<BossAI>(true) != null) SwitchWeapon(1);
        else SwitchWeapon(0);
    }
    private void OnEnable() {
        _isDashing = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
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
            if(levelManager.continues != 0)levelManager.Respawn();
            hitpoints = maxhitpoints;
            levelManager.continues -= 1;
        }
        if (knockbackcounter > 0) {
            knockbackcounter -= Time.deltaTime;
            movement = knockbackdir.normalized * knockbackforce;
        }
    }
    private void FixedUpdate() {
        BossAI boss = FindObjectOfType<BossAI>(true);
        if (boss != null && boss.phasechangeimg.gameObject.activeSelf) return;
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Enemy>() && !dead && !collision.gameObject.GetComponent<Enemy>().dead && collision.gameObject.GetComponent<Enemy>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            TakeHit(collision.gameObject.GetComponent<Enemy>().damage);
            knockbackdir = transform.position - collision.transform.position;
            Knockback();
            AudioManager.instance.PlaySFX(hitsound);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Slow")) {
            moveSpeed = Mathf.Lerp(moveSpeed, moveSpeed - 1, 1f * Time.deltaTime);
            if (moveSpeed < 0) moveSpeed = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Trap>() && !dead) {
            TakeHit(collision.GetComponent<Trap>().damage);
            knockbackdir = transform.position - collision.transform.position;
            Knockback();
            AudioManager.instance.PlaySFX(hitsound);
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
        if (collision.name.Contains("Double Barrel Gun Pick Up")) {
            AudioManager.instance.PlaySFX(AudioManager.instance.healthSound);
            SwitchWeapon(1);
            End end = FindObjectOfType<End>();
            Destroy(collision.gameObject);
            StartCoroutine(MiniBossDefeated());
            end.transform.position = collision.transform.position + new Vector3(0, 10, 0);
        }
        if (collision.GetComponent<EnemyProjectile>() && !dead) {
            TakeHit(collision.gameObject.GetComponent<EnemyProjectile>().damage);
            knockbackdir = transform.position - collision.transform.position;
            Knockback();
            AudioManager.instance.PlaySFX(hitsound);
            StartCoroutine(DamageFeedback());
            StartCoroutine(Invulnerability());
        }
    }
    IEnumerator MiniBossDefeated() {
        yield return new WaitForSeconds(0.5f);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Slow")) {
            moveSpeed = ogmovespeed;
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
    }
    public void Dash() {
        if (!dead && Time.time >= nextdashtime) {
            _canDash = true;
            nextdashtime = Time.time + _dashingTime;
        }
        if (_canDash) {
            _isDashing = true;
            AudioManager.instance.PlaySFX(dashsound);
            _canDash = false;
            _dashingDir = movement.normalized;
        }
        if (_isDashing) {
            movement = _dashingDir.normalized * _dashingVelocity;
            StartCoroutine(Invulnerability());
            return;
        }
    }
    public void SwitchWeapon(int weaponnum) {
        currentweapon = weaponnum;
        foreach(var i in weapons) {
            i.gameObject.SetActive(false);
            i.transform.parent.gameObject.SetActive(false);
            weapons[currentweapon].gameObject.SetActive(true);
            weapons[currentweapon].transform.parent.gameObject.SetActive(true);
        }
    }
    void Knockback() {
        knockbackcounter = knockbacklength;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : Enemy
{
    public GameObject projectile;
    float nextfiretime;
    public float firerate;
    public Transform firept;
    bool isShooting;
    public EnemySpawner[] spawners;
    public GameObject shield,shield2;
    // Start is called before the first frame update
    // Update is called once per frame
    protected override void Start() {
        gameObject.SetActive(false);
        isShooting = false;
        base.Start();
    }
    protected override void FixedUpdate() {
        if (canMove) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
    }
    protected override void Update() {
        anim.SetBool("Shooting", isShooting);
        if (!canMove) return;
        if (shield.GetComponent<BossShield>().circleCollider.enabled || shield2.GetComponent<BossShield>().circleCollider.enabled) {
            foreach(var i in spawners) {
                i.BossSpawn();
            }
        }
        if (hitpoints <= maxhitpoints * .7f && hitpoints > maxhitpoints * .3f) {
            shield.GetComponent<BossShield>().sr.enabled = true;
            shield.GetComponent<BossShield>().circleCollider.enabled = true;
        } 
        else if (hitpoints < maxhitpoints * .3f) {
            shield2.GetComponent<BossShield>().sr.enabled = true;
            shield2.GetComponent<BossShield>().circleCollider.enabled = true;
        }
        Fire();
        base.Update();
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (shield.GetComponent<BossShield>().circleCollider.enabled) return;
        if (shield2.GetComponent<BossShield>().circleCollider.enabled) return;
        if (collision.GetComponent<ExplodingProjectile>() && !dead && collision.tag != "Enemy") {
            TakeHit(Mathf.Abs(collision.GetComponent<ExplodingProjectile>().damage) * 2);
            AudioManager.instance.PlaySFX(hitsound);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab) {
                var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
            }
        }
        base.OnTriggerEnter2D(collision);
    }
    private void Move(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
    }
    public override IEnumerator Death() {
        
        return base.Death();
    }
    void Fire() {
        if (Time.time < nextfiretime) return;
        Instantiate(projectile, firept.position, firept.rotation);
        isShooting = true;
        nextfiretime = Time.time + firerate;
    }
    protected override void Damaged(Collider2D collision) {
        TakeHit(Mathf.Abs(collision.GetComponent<ProjectileScript>().damage));
        AudioManager.instance.PlaySFX(hitsound);
        StartCoroutine(DamageFeedback());
        if (floatingTextPrefab) {
            var floatingtext = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            floatingtext.GetComponent<TMPro.TextMeshPro>().text = collision.GetComponent<ProjectileScript>().damage.ToString();
        }
    }
}

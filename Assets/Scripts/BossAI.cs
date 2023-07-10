using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : Enemy
{
    public GameObject projectile;
    float nextfiretime;
    public float firerate;
    public Transform firept;
    public bool isShooting;
    public EnemySpawner[] spawners;
    //public List<Enemy> enemies;
    public GameObject shield,shield2,mask;
    public AudioClip shootsound;
    public Sprite[] masksprites;
    public bool shieldup;
    public bool phase1 = false;
    public bool phase2 = false;
    //public bool shieldcostart = false;
    // Start is called before the first frame update
    // Update is called once per frame
    protected override void Start() {
        gameObject.SetActive(false);
        isShooting = false;
        shieldup = false;
        base.Start();
    }
    protected override void FixedUpdate() {
        if (canMove) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
    }
    protected override void Update() {
        anim.SetBool("Shooting", isShooting);
        anim.SetBool("Shield", shieldup);
        if (!canMove) return;
        //isShooting = true;
        if (shield.GetComponent<BossShield>().circleCollider.enabled || shield2.GetComponent<BossShield>().circleCollider.enabled) {
            foreach(var i in spawners) {
                i.BossSpawn();
            }
           // if(enemies.Count < 5)enemies.AddRange(FindObjectsOfType<RangedEnemy>());
        }
        if (hitpoints <= maxhitpoints * .7f && hitpoints > maxhitpoints * .3f) {
            if (!phase1) {
                phase1 = true;
                mask.GetComponent<SpriteRenderer>().sprite = masksprites[0];
                StartCoroutine(ShieldUp(shield.GetComponent<BossShield>()));
            }
            //if (shield.GetComponent<BossShield>().circleCollider.enabled) StopCoroutine(ShieldUp(shield.GetComponent<BossShield>()));
            //shieldcostart = true;
            //shieldcostart = false;
            //shieldcostart = true;
        } else if (hitpoints < maxhitpoints * .3f) {
            if (!phase2) {
                phase2 = true;
                mask.GetComponent<SpriteRenderer>().sprite = masksprites[1];
                StartCoroutine(ShieldUp(shield2.GetComponent<BossShield>()));
            }
            
            //if (shield2.GetComponent<BossShield>().circleCollider.enabled) StopCoroutine(ShieldUp(shield2.GetComponent<BossShield>()));
            //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //shieldcostart = false;
            //shieldcostart = true;
        }
        if (hitpoints < maxhitpoints * .1f) {
            mask.SetActive(false);
        }
        //Fire();
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
        //foreach(var i in enemies) {
        //    if(!i.dead) i.hitpoints = 0;
        //}
        return base.Death();
    }
    public void Fire() {
        if (Time.time < nextfiretime) return;
        Instantiate(projectile, firept.position, firept.rotation);
        AudioManager.instance.PlaySFX(shootsound);
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
    IEnumerator ShieldUp(BossShield shield) {
        shield.sr.enabled = true;
        shield.circleCollider.enabled = true;
        isShooting = false;
        shieldup = true;
        yield return new WaitForSeconds(1);
        isShooting = true;
        shieldup = false;
    }
    /*public void ResetSpawners() {
        foreach (var i in spawners) {
            i.canSpawn = true;
        }
        
    }*/
}

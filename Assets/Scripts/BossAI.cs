using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : Enemy
{
    public GameObject[] projectile;
    float nextfiretime;
    public float firerate;
    public Transform firept;
    public bool isShooting;
    public EnemySpawner[] spawners;
    public GameObject shield,shield2,mask;
    public AudioClip shootsound,deathsound,phasechangesound;
    public Sprite[] masksprites,phaseimgs;
    public bool shieldup;
    public Phases phase;
    int bulletsfired = 0,projectileindex=0;
    public Image phasechangeimg;
    [SerializeField]bool death;
    public enum Phases { start,shield1,shield2}
    // Start is called before the first frame update
    // Update is called once per frame
    protected override void Start() {
        gameObject.SetActive(false);
        isShooting = false;
        shieldup = false;
        phase = Phases.start;
        death = false;
        base.Start();
    }
    protected override void FixedUpdate() {
        if (canMove && !phasechangeimg.gameObject.activeSelf) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
    }
    protected override void Update() {
        anim.SetBool("Shooting", isShooting);
        anim.SetBool("Shield", shieldup);
        if (!canMove) return;
        //isShooting = true;
        firerate = anim.GetCurrentAnimatorStateInfo(0).length - anim.GetCurrentAnimatorStateInfo(0).length * anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (shield.GetComponent<BossShield>().circleCollider.enabled || shield2.GetComponent<BossShield>().circleCollider.enabled) {
            foreach(var i in spawners) {
                i.BossSpawn();
            }
        }
        switch (phase) {
            case Phases.start:
                if (hitpoints <= maxhitpoints * .7f && hitpoints > maxhitpoints * .3f) {
                    PhaseChange();
                }
                break;
            case Phases.shield1:
                if (hitpoints < maxhitpoints * .3f) {
                    PhaseChange();
                }
                break;
            case Phases.shield2:
                if (hitpoints < maxhitpoints * .1f) {
                    PhaseChange();
                }
                break;
        }
        //Fire();
        base.Update();
    }
    protected override void CheckDeath() {
        if(hitpoints <= 0 && !death) {
            StartCoroutine(Death());
            death = true;
        }
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
        if (dead && collision.GetComponent<Player>()) print("cutscene");
        base.OnTriggerEnter2D(collision);
    }
    private void Move(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (movespeed * Time.deltaTime * direction));
    }
    public override IEnumerator Death() {
        rb.velocity = Vector2.zero;
        if (!dead) dead = true;
        healthbar.gameObject.SetActive(false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(Instantiate(deathvfx, transform.position, transform.rotation), 5f);
        GetComponent<Collider2D>().isTrigger = true;
        //Destroy(gameObject);
        //for (int i = 0; i < Random.Range(1, dropamt + 1); i++) {
        //    Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
        //}
       // FindObjectOfType<End>().transform.position = player.transform.position;
        //yield return base.Death();
    }
    public void PlayDeathSound() {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySFX(deathsound);
    }
    public void Fire() {
        if (Time.time < nextfiretime) return;
        if (phasechangeimg.gameObject.activeSelf) return;
        Instantiate(projectile[projectileindex], firept.position, firept.rotation);
        bulletsfired++;
        AudioManager.instance.PlaySFX(shootsound);
        nextfiretime = Time.time + firerate;
        if (bulletsfired % 3 == 0) {
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown() {
        yield return new WaitForSeconds(.25f);
        isShooting = false;
        yield return new WaitForSeconds(1f);
        isShooting = true;
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
        yield return new WaitForSeconds(1f);
        isShooting = true;
        shieldup = false;
    }
    public void ResetSpawners() {
        foreach (var i in spawners) {
            i.enemiesspawned = 0;
            i.iter += 1;
        }
    }
    Phases PhaseChange() {
        switch (phase) {
            case Phases.start:
                phase = Phases.shield1;
                mask.GetComponent<SpriteRenderer>().sprite = masksprites[0];
                phasechangeimg.sprite = phaseimgs[0];
                StartCoroutine(PhaseImg());
                StartCoroutine(ShieldUp(shield.GetComponent<BossShield>()));
                projectileindex += 1;
                break;
            case Phases.shield1:
                phase = Phases.shield2;
                ResetSpawners();
                mask.GetComponent<SpriteRenderer>().sprite = masksprites[1];
                phasechangeimg.sprite = phaseimgs[1];
                StartCoroutine(PhaseImg());
                StartCoroutine(ShieldUp(shield2.GetComponent<BossShield>()));
                break;
            case Phases.shield2:
                mask.SetActive(false);
                //phasechangeimg.sprite = phaseimgs[2];
                //StartCoroutine(PhaseImg());
                break;
        }
        return phase;
    }
    IEnumerator PhaseImg() {
        phasechangeimg.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        phasechangeimg.gameObject.SetActive(false);
    }
}

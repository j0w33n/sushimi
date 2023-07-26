using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    float nextfiretime;
    public float firerate;
    public Transform firept;
    public AudioClip shootsound;
    public bool isShooting;
    protected override void Start() {
        isShooting = true;
        base.Start();
    }
    protected override void FixedUpdate() {
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
    }
    protected override void Update() {
        anim.SetBool("Shooting", isShooting);
        if(FindObjectOfType<BossAI>() != null) {
            if (FindObjectOfType<BossAI>().dead) hitpoints = 0;
        }
        base.Update();
    }
    public void Fire() {
        if (Time.time < nextfiretime) return;
        if (dead) return;
        if (FindObjectOfType<BossAI>(true).phasechangeimg.gameObject.activeSelf) return;
        Instantiate(projectile, firept.position, firept.rotation);
        AudioManager.instance.PlaySFX(shootsound);
        nextfiretime = Time.time + firerate;
    }
}

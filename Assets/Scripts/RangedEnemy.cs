using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    float nextfiretime;
    public float firerate;
    public Transform firept;
    protected override void FixedUpdate() {
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
    }
    protected override void Update() {
        Fire();
        base.Update();
    }
    void Fire() {
        if (Time.time < nextfiretime) return;
        if (dead) return;
        Instantiate(projectile, firept.position, firept.rotation);
        nextfiretime = Time.time + firerate;
    }
}

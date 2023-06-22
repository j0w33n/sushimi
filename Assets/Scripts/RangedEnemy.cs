using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    float nextfiretime;
    public float firerate;
    protected override void FixedUpdate() {
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
    }
    protected override void Update() {
        if (Time.time < nextfiretime) return;
        Instantiate(projectile, transform.position, transform.rotation);
        nextfiretime = Time.time + firerate;
        base.Update();
    }
}

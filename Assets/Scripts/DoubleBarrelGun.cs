using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelGun : Weapon
{
    // Start is called before the first frame update
    protected override void Start() {
        ammo = PlayerPrefs.GetInt("Max Ammo (Double)", 12);
        base.Start();
    }
    public override void Fire() {
        if (Time.time < nextfiretime) return;
        if (FindObjectOfType<BossAI>().phasechangeimg.gameObject.activeSelf) return;
        if (slow) {
            Instantiate(slowingprojectile, firept.position, firept.rotation);
            Instantiate(slowingprojectile, firept.position + new Vector3(0, 1, 0), firept.rotation);
            AudioManager.instance.PlaySFX(slowshootsound,true);
        } 
        else if (explode) {
            Instantiate(explodingprojectile, firept.position, firept.rotation);
            Instantiate(explodingprojectile, firept.position + new Vector3(0, 1, 0), firept.rotation);
            AudioManager.instance.PlaySFX(explodingshootsound,true);
        } 
        else {
            Instantiate(projectileprefab, firept.position, firept.rotation);
            Instantiate(projectileprefab, firept.position + new Vector3(0, 1, 0), firept.rotation);
            AudioManager.instance.PlaySFX(normalshootsound,true);
        }
        ammo-=2;
        nextfiretime = Time.time + firerate;
    }
    // Update is called once per frame
}

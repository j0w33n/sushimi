using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class BaseWeapon : Weapon
{

    protected override void Start() {
        ammo = PlayerPrefs.GetInt("Max Ammo (Base)", 6);
        base.Start();
    }
    public override void Fire() {

        if (Time.time < nextfiretime) return;
        //StopCoroutine(Reload());
        if (slow) {
            Instantiate(slowingprojectile, firept.position, firept.rotation);
            gunTransform.rotation = gunTransform.rotation;
            AudioManager.instance.PlaySFX(slowshootsound,true);
        } 
        else if (explode) {
            Instantiate(explodingprojectile, firept.position, firept.rotation);
            gunTransform.rotation = gunTransform.rotation;
            AudioManager.instance.PlaySFX(explodingshootsound,true);
        } 
        else {
            Instantiate(projectileprefab, firept.position, firept.rotation);
            gunTransform.rotation = gunTransform.rotation;
            AudioManager.instance.PlaySFX(normalshootsound,true);
        }
        ammo--;
        nextfiretime = Time.time + firerate;
    }
}

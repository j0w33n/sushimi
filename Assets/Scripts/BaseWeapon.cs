using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class BaseWeapon : Weapon
{

    public override void Fire() {

        if (Time.time < nextfiretime) return;
        //StopCoroutine(Reload());
        if (slow) {
            Instantiate(slowingprojectile, firept.position, firept.rotation);
            gunTransform.rotation = gunTransform.rotation;
            audio.PlayOneShot(slowshootsound);
        } 
        else if (explode) {
            Instantiate(explodingprojectile, firept.position, firept.rotation);
            gunTransform.rotation = gunTransform.rotation;
            audio.PlayOneShot(explodingshootsound);
        } 
        else {
            Instantiate(projectileprefab, firept.position, firept.rotation);
            gunTransform.rotation = gunTransform.rotation;
            audio.PlayOneShot(normalshootsound);
        }
        ammo--;
        nextfiretime = Time.time + firerate;
    }
}

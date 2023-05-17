using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : Weapon
{
    public override void Fire() {
        if (Time.time < nextfiretime) return;
        if (slow) {
            Instantiate(slowingprojectile, firept.position, firept.rotation);
            audio.PlayOneShot(slowshootsound);
        } 
        else if (explode) {
            Instantiate(explodingprojectile, firept.position, firept.rotation);
            audio.PlayOneShot(explodingshootsound);
        } 
        else {
            Instantiate(projectileprefab, firept.position, firept.rotation);
            audio.PlayOneShot(normalshootsound);
        }
        ammo--;
        nextfiretime = Time.time + firerate;
    }
}

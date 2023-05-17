using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelGun : Weapon
{
    // Start is called before the first frame update
    public override void Fire() {
        if (Time.time < nextfiretime) return;
        if (slow) {
            Instantiate(slowingprojectile, firept.position, firept.rotation);
            Instantiate(slowingprojectile, firept.position + new Vector3(0, 1, 0), firept.rotation);
            audio.PlayOneShot(slowshootsound);
        } 
        else if (explode) {
            Instantiate(explodingprojectile, firept.position, firept.rotation);
            Instantiate(explodingprojectile, firept.position + new Vector3(0, 1, 0), firept.rotation);
            audio.PlayOneShot(explodingshootsound);
        } 
        else {
            Instantiate(projectileprefab, firept.position, firept.rotation);
            Instantiate(projectileprefab, firept.position + new Vector3(0, 1, 0), firept.rotation);
            audio.PlayOneShot(normalshootsound);
        }
        ammo--;
        nextfiretime = Time.time + firerate;
    }
    // Update is called once per frame
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public abstract class Weapon : MonoBehaviour
{
    public Transform firept;
    LevelManager levelManager;
    public GameObject projectileprefab,slowingprojectile,explodingprojectile;
    protected float nextfiretime;
    public float firerate;
    [HideInInspector]public Vector2 joystickposition;
    public int ammo;
    public int maxammo;
    public float reloadspeed;
    private bool isreloading = false;
    protected AudioSource audio;
    public AudioClip normalshootsound,slowshootsound,explodingshootsound;
    public bool slow,explode;

    public Transform gunTransform;
    public VirtualJoystick joystick;
    public float rotationSpeed = 30f;

    protected virtual void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void OnEnable() {
        isreloading = false;
    }
    protected void Update() {
        gunTransform.rotation = gunTransform.rotation;
        levelManager.ammobar.value = ammo;
        levelManager.ammobar.maxValue = maxammo;
        joystickposition = new Vector2(VirtualJoystick.GetAxis("Horizontal", 1), VirtualJoystick.GetAxis("Vertical", 1));
        /*if (isreloading) {
            return;
        }*/
        if (((ammo < maxammo && joystickposition.magnitude < 0.1f) || ammo <= 0) && !isreloading) {
            StartCoroutine(Reload());
            //return;
        }
        if (joystickposition.magnitude > 0.5f) {
           if((isreloading && ammo > 0) || ammo > 0) {
                Fire();
           }
        }

        Vector2 joystickPosition = new Vector2(VirtualJoystick.GetAxis("Horizontal", 1), VirtualJoystick.GetAxis("Vertical", 1));

        if (joystickPosition.magnitude > 0.5f) {
            float targetAngle = Mathf.Atan2(joystickPosition.y, joystickPosition.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Flip the gun sprite when facing left
            if (joystickPosition.x < 0) {
                Vector3 localScale = gunTransform.localScale;
                localScale.y = -Mathf.Abs(localScale.y);
                gunTransform.localScale = localScale;
            } 
            else {
                Vector3 localScale = gunTransform.localScale;
                localScale.y = Mathf.Abs(localScale.y);
                gunTransform.localScale = localScale;
            }
        }
        }
    public abstract void Fire();
    public IEnumerator Reload() {
        gunTransform.rotation = gunTransform.rotation;
        isreloading = true;
        while(ammo < maxammo) {
            //if (joystickposition.magnitude > .1f && !isreloading) yield break;
            yield return new WaitForSeconds(reloadspeed / maxammo);
            ammo++;
            if (ammo > maxammo) ammo = maxammo;
            AudioManager.instance.PlaySFX(AudioManager.instance.reloadSound);
        }
        isreloading = false;
    }
}

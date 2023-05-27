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
    // Start is called before the first frame update
    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ammo = maxammo;
        levelManager.ammobar.maxValue = maxammo;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void OnEnable() {
        isreloading = false;
    }
    protected void Update()
    {
        levelManager.ammobar.value = ammo;
        joystickposition = new Vector2(VirtualJoystick.GetAxis("Horizontal", 1), VirtualJoystick.GetAxis("Vertical", 1));
        if (isreloading) {
            return;
        }
        if ((ammo < maxammo && joystickposition.magnitude < 0.1f) || ammo <= 0) {
            StartCoroutine(Reload());
            return;
        }
        if (joystickposition.magnitude > 0.1f && ammo > 0) {
            Fire();
        }
    }
    public abstract void Fire();
    public IEnumerator Reload() {
        isreloading = true;
        while(ammo < maxammo) {
            yield return new WaitForSeconds(reloadspeed / maxammo);
            ammo++;
            AudioManager.instance.PlaySFX(AudioManager.instance.reloadSound);
        }
        isreloading = false;
    }
}

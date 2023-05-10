using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class ShootingScript : MonoBehaviour
{
    public Transform firept;
    LevelManager levelManager;
    public GameObject projectileprefab;
    private float nextfiretime;
    public float firerate;
    public float rotationspeed = 180;
    [HideInInspector]public Vector2 joystickposition;
    [SerializeField]private int ammo;
    public int maxammo;
    public float reloadspeed;
    private bool isreloading = false;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ammo = maxammo;
        levelManager.ammobar.maxValue = maxammo;
    }

    // Update is called once per frame
    void Update()
    {
        levelManager.ammobar.value = ammo;
        joystickposition = new Vector2(VirtualJoystick.GetAxis("Horizontal", 1), VirtualJoystick.GetAxis("Vertical", 1));
        if (isreloading) {
            return;
        }
        if (ammo <= 0) {
            StartCoroutine(Reload());
            return;
        }
        if (joystickposition.magnitude > 0.1f && ammo > 0) {
            Fire();
        }
    }
    public void Fire() {
        if (Time.time < nextfiretime) return;
        Instantiate(projectileprefab, transform.position, Quaternion.identity);
        ammo--;
        nextfiretime = Time.time + firerate;
    }
    public IEnumerator Reload() {
        isreloading = true;
        while(ammo < maxammo) {
            yield return new WaitForSeconds(reloadspeed);
            ammo++;
        }
        isreloading = false;
    }
}

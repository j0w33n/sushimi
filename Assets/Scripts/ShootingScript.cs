using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firept;
    public GameObject bulletprefab;
    private float nextfiretime;
    public float firerate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }
    void Fire() {
        if (Time.time < nextfiretime) return;
        Destroy(Instantiate(bulletprefab, transform.position, transform.rotation), 3f);
        nextfiretime = Time.time + firerate;
    }
}

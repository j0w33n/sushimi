using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class ShootingScript : MonoBehaviour
{
    public Transform firept;
    Player player;
    public GameObject instprefab;
    private float nextinsttime;
    public float instrate;
    public float rotationspeed = 180;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Fire1")) {
            Generate();
        }*/
        transform.Rotate(0, 0, rotationspeed * VirtualJoystick.GetAxis("Horizontal", 1) * Time.deltaTime);
    }
    public void Generate() {
        if (Time.time < nextinsttime) return;
        Instantiate(instprefab, transform.position, transform.rotation);
        nextinsttime = Time.time + instrate;
    }
}

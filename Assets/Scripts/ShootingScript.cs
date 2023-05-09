using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class ShootingScript : MonoBehaviour
{
    public Transform firept;
    LevelManager levelManager;
    public GameObject instprefab;
    private float nextinsttime;
    public float instrate;
    public float rotationspeed = 180;
    public Vector2 joystickposition;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        joystickposition = new Vector2(VirtualJoystick.GetAxis("Horizontal", 1), VirtualJoystick.GetAxis("Vertical", 1));
        if (joystickposition.magnitude > 0.1f) {
            Generate();
        }
        // we want to rotate around the z-axis according to the right joystick's position
        //transform.Rotate(0, 0, joystickposition.);
    }
    public void Generate() {
        if (Time.time < nextinsttime) return;
        Instantiate(instprefab, transform.position, Quaternion.identity);
        nextinsttime = Time.time + instrate;
    }
}

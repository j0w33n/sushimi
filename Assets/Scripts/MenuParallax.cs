using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField]
    private float variation = 1f;

    private Gyroscope gyro;


    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((float)System.Math.Round(gyro.rotationRateUnbiased.y, 1) * variation, 0f, 0f);
    }
}

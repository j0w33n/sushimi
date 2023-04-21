using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 2, 0); //eh saiful i copy this dmg text script from youtube for 3D game ~YY
    public Vector3 randomiseIntensity = new Vector3(0.5, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-randomiseIntensity.x, randomiseIntensity.x),
        Random.Range(-randomiseIntensity.y, randomiseIntensity.y),
        Random.Range(-randomiseIntensity.z, randomiseIntensity.z));   //is vector 3 even needed coz we doing 2D I dont dare anyhow
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

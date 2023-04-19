using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyprefab;
    public float spawnrate;
    private float nextspawntime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
    void Spawn() {
        if (Time.time < nextspawntime) return;
        Instantiate(enemyprefab,transform.position,transform.rotation);
        nextspawntime = Time.time + spawnrate;
    }
}

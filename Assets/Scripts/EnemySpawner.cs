using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    // int enemiesspawned;
    //public int enemiestospawn;
    // Start is called before the first frame update
    public GameObject instprefab;
    private float nextinsttime;
    public float instrate;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Generate();
        //Spawn();
    }
    public void Generate() {
        if (Time.time < nextinsttime) return;
        Instantiate(instprefab, transform.position, transform.rotation);
        nextinsttime = Time.time + instrate;
    }
    /*void Spawn() {
        if (enemiesspawned < enemiestospawn) {
            Generate();
            enemiesspawned += 1;
        } else return;
    }*/
}

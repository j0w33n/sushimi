using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    int enemiesspawned;
    public int enemiestospawn;
    // Start is called before the first frame update
    public GameObject instprefab;
    public float instrate;
    LevelManager levelManager;
    Player player;
    float nextinsttime;
    void Start()
    {
       levelManager = FindObjectOfType<LevelManager>();
       player = FindObjectOfType<Player>();
       
    }

    // Update is called once per frame
    void Update()
    {
       Spawn();
    }
   void Spawn() {
        if (enemiesspawned <= enemiestospawn - 1 && !player.dead) {
            if (Time.time < nextinsttime) return;
            Instantiate(instprefab, transform.position, transform.rotation);
            nextinsttime = Time.time + instrate;
            enemiesspawned++;
        }
    }
}

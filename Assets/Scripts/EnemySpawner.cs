using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GeneralFunctions {

   // int enemiesspawned;
    //public int enemiestospawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Generate();
        //Spawn();
    }
    /*void Spawn() {
        if (enemiesspawned < enemiestospawn) {
            Generate();
            enemiesspawned += 1;
        } else return;
    }*/
}

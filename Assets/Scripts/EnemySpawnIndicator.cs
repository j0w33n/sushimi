using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnIndicator : MonoBehaviour
{
    public float indicatorLength;
    private Animator anim;

    public GameObject enemyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        anim.speed = indicatorLength;
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(enemyToSpawn, transform.position, transform.rotation);
    }
}

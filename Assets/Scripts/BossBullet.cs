using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : ProjectileScript
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = 4f; //Homing Bullet needs to be slower
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomwaves, roomnum;
    public bool roomstart;
    public GameObject[] enemyspawns;
    public GameObject exit;
    Player player;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            RoomTrigger();
        }
    }
    void RoomTrigger() {
        roomstart = true;
        levelManager.waves = roomwaves;
        levelManager.currentroom = gameObject;
        for (int i = 0; i < enemyspawns.Length; i++) {
            enemyspawns[i].SetActive(true);
            enemyspawns[i].GetComponent<EnemySpawner>().canSpawn = true;
        }
        player.respawnpoint = transform.position;
        exit.SetActive(true);
        gameObject.SetActive(false);
    }  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject[] enemyspawns;
    public GameObject exit, tutorialmsg;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Player>() && gameObject.tag != "Trigger") {
            WaveTrigger();
        }
        else if (collision.GetComponent<Player>() && gameObject.tag == "Trigger") {
            tutorialmsg.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Player>() && gameObject.tag == "Trigger") {
            tutorialmsg.SetActive(false);
        }
    }
    void WaveTrigger() {
        for(int i = 0; i < enemyspawns.Length; i++) {
            enemyspawns[i].SetActive(true);
            enemyspawns[i].GetComponent<EnemySpawner>().canSpawn = true;
        }
        player.respawnpoint = transform.position;
        exit.SetActive(true);
        gameObject.SetActive(false);
    }
}

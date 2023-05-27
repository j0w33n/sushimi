using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int roomwaves, roomnum;
    public bool roomstart;
    public GameObject[] myenemyspawns;
    public GameObject[] exit,entrance;
    Player player;
    LevelManager levelManager;
    WaveClearText clearText;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        clearText = FindObjectOfType<WaveClearText>();
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
        levelManager.wavecomplete = false;
        levelManager.enemyspawns.Clear();
        clearText.gameObject.SetActive(true);
        //clearText.GetComponent<Text>().color = new Color32(0,0,0,255);
        foreach (GameObject i in myenemyspawns) {
            levelManager.enemyspawns.Add(i.GetComponent<EnemySpawner>());
        }
        foreach (var i in levelManager.enemyspawns) {
            levelManager.totalenemies += i.enemiestospawn[i.currentwave];
        }
        for (int i = 0; i < myenemyspawns.Length; i++) {
            //myenemyspawns[i].SetActive(true);
            myenemyspawns[i].GetComponent<EnemySpawner>().canSpawn = true;
        }
        player.respawnpoint = transform.position;
        AudioManager.instance.PlaySFX(AudioManager.instance.exitSound);
        foreach(var i in exit) {
            i.SetActive(true);
        }
        gameObject.SetActive(false);
    }  
}

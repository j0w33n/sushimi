using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1End : MonoBehaviour
{
    [SerializeField]GameObject[] room6,room7,room8;
    [SerializeField]bool room6clear, room7clear, room8clear;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        room6 = GameObject.FindGameObjectsWithTag("L1R6");
        room7 = GameObject.FindGameObjectsWithTag("L1R7");
        room8 = GameObject.FindGameObjectsWithTag("L1R8");
        room6clear = false;
        room7clear = false;
        room8clear = false;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var i in room6) {
            if (!i.activeSelf && !levelManager.currentroom.GetComponent<Room>().roomstart) room6clear = true;
        }
        foreach(var i in room7) {
            if (!i.activeSelf && !levelManager.currentroom.GetComponent<Room>().roomstart) room7clear = true;
        }
        foreach(var i in room8) {
            if (!i.activeSelf && !levelManager.currentroom.GetComponent<Room>().roomstart) room8clear = true;
        }
        if(room8clear && room7clear && room6clear) {
            AudioManager.instance.PlaySFX(AudioManager.instance.entranceSound);
            gameObject.SetActive(false);
        }
    }
}

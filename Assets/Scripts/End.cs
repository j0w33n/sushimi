using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    EventTrigger levelend;// Start is called before the first frame update
    void Start()
    {
        levelend = GameObject.Find("LevelEnd").GetComponent<EventTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndScreen() {
        StartCoroutine(levelend.fade.Appear());
        //yield return new WaitForSeconds(3);
        SceneManager.LoadScene(levelend.levelManager.leveltoload);
        PlayerPrefs.SetInt("Parts", levelend.levelManager.parts);
        PlayerPrefs.SetInt("Total Enemies Killed", levelend.levelManager.totalenemieskilled);
        PlayerPrefs.SetFloat("Max Health", levelend.player.maxhitpoints);
        PlayerPrefs.SetInt("Max Ammo (Base)", levelend.player.GetComponentsInChildren<Weapon>(true)[0].maxammo);
        PlayerPrefs.SetInt("Max Ammo (Double)", levelend.player.GetComponentsInChildren<Weapon>(true)[1].maxammo);
        PlayerPrefs.SetInt("Current Room", 0);
    }
}

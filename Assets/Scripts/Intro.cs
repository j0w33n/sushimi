using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    
    public float waittime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
        if (PlayerPrefs.GetInt("HasPlayedBefore", 0) == 0) {
            PlayerPrefs.SetFloat("SFX Volume", 1f);
            PlayerPrefs.SetFloat("Music Volume", 1f);
            PlayerPrefs.SetString("Current Level", "");
            PlayerPrefs.SetInt("Current Room", 0);
            PlayerPrefs.SetInt("HasPlayedBefore", 1);
        }
    }
    private void Awake() {
        //PlayerPrefs.SetInt("HasPlayedBefore", 1);
        // calibration: comment out line 19 then build, play to level 1. Exit game and reopen, press continue to check if playerprefs are kept. then uncomment lines 26-28 and build again before showing terence and jiayan.
        // reset the damage of all projectiles to 1 as well.
        if (!Application.isEditor && PlayerPrefs.GetInt("HasPlayedBefore") == 1) { 
            PlayerPrefs.SetInt("HasPlayedBefore", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        print("Played:" + PlayerPrefs.GetInt("HasPlayedBefore"));
        print("SFX:" + PlayerPrefs.GetFloat("SFX Volume", 1f));
        print("Music:" + PlayerPrefs.GetFloat("Music Volume", 1f));
        print("Room:" + PlayerPrefs.GetInt("Current Room", 0));
        print("Level:" + PlayerPrefs.GetString("Current Level", ""));
    }
    IEnumerator LoadScene() {
        yield return new WaitForSeconds(waittime);
        FindObjectOfType<SceneController>().MainMenu();
    }
}

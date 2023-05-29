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
        // calibration: after testing, comment out line 19 and uncomment out lines 26-28,comment out !Application.isEditor && to verify value of hasplayedbefore and run. after verifying hasplayedbefore == 0, uncomment line 19 and comment out lines 26-28 and build.
        // reset the damage of all projectiles to 1 as well as turn mobile control back on.
        /*if (!Application.isEditor && PlayerPrefs.GetInt("HasPlayedBefore") == 1) { 
            PlayerPrefs.SetInt("HasPlayedBefore", 0);
        }*/
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

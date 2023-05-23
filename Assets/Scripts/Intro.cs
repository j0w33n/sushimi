using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public AudioClip menumusic;
    public float waittime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
        if (PlayerPrefs.GetInt("HasPlayedBefore", 0) == 0) {
            PlayerPrefs.SetInt("Parts", 0);
            PlayerPrefs.SetInt("Total Enemies Killed", 0);
        }
    }
        private void Awake() {
        //if (PlayerPrefs.GetInt("HasPlayedBefore") != 1)PlayerPrefs.SetInt("HasPlayedBefore", 0);
        if (!Application.isEditor && PlayerPrefs.GetInt("HasPlayedBefore") != 1) { // check if in build
            PlayerPrefs.SetInt("HasPlayedBefore", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadScene() {
        yield return new WaitForSeconds(waittime);
        StartCoroutine(LevelManager.SwitchMusic(menumusic));
        SceneManager.LoadScene("MainMenu");
    }
}

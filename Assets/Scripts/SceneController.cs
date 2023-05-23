using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start() {
       // var var = FindObjectOfType<AudioManager>();
       // if (var.GetComponent<AudioSource>().isPlaying) return;
       // StartCoroutine(LevelManager.SwitchMusic(menumusic));
    }

    // Update is called once per frame
    void Update() {

    }
    public void NewGame() {
        SceneManager.LoadScene("Tutorial");
    }
    public void Credits() {
       SceneManager.LoadScene("Credits");
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
        //StartCoroutine(LevelManager.SwitchMusic(menumusic));
    }
    public void Continue() {

    }
    public void Settings() {
        SceneManager.LoadScene("Settings");
        //StartCoroutine(LevelManager.SwitchMusic(menumusic));
    }
}

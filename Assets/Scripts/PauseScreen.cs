using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {
    public Player player;
    public LevelManager levelManager;
    public GameObject pausescreen,settingsscreen;
    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void PauseGame() {
        //levelManager.levelmusic.Pause();
        Time.timeScale = 0; // freeze game
        pausescreen.SetActive(true);
        player.canMove = false;
    }
    public void ResumeGame() {
        Time.timeScale = 1;
        pausescreen.SetActive(false);
        settingsscreen.SetActive(false);
        player.canMove = true;
        //levelManager.levelmusic.UnPause();
    }
    public void BackToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().PlayMusic(AudioManager.instance.menumusic);
    }
    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Settings() {
        settingsscreen.SetActive(true);
        pausescreen.SetActive(false);
    }
    /*public void Pause() {
        settingsscreen.SetActive(false);
        pausescreen.SetActive(true);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public AudioClip menumusic;
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
        SceneManager.LoadScene("Pre-Story");
        PlayerPrefs.SetInt("Parts", 0);
        PlayerPrefs.SetInt("Total Enemies Killed", 0);
        PlayerPrefs.SetInt("Current Room", 0);
        PlayerPrefs.SetFloat("Max Health", 5);
        PlayerPrefs.SetInt("Max Ammo (Base)", 6);
        PlayerPrefs.SetInt("Max Ammo (Double)", 12);
        PlayerPrefs.SetInt("hasTranslator", 0);
        PlayerPrefs.SetFloat("TotalParts", 0);
        PlayerPrefs.SetFloat("TotalTime", 0);
        AudioManager.instance.StopMusic();
    }

    public void CleanReset() {
        SceneManager.LoadScene("Pre-Story");
        PlayerPrefs.SetInt("Parts", 0);
        PlayerPrefs.SetInt("Total Enemies Killed", 0);
        PlayerPrefs.SetInt("Current Room", 0);
        PlayerPrefs.SetFloat("Max Health", 5);
        PlayerPrefs.SetInt("Max Ammo (Base)", 6);
        PlayerPrefs.SetInt("Max Ammo (Double)", 12);
        PlayerPrefs.SetInt("hasTranslator", 0);
        PlayerPrefs.SetFloat("TotalParts", 0);
        PlayerPrefs.SetFloat("TotalTime", 0);
        PlayerPrefs.SetFloat("Level0Parts", 0);
        PlayerPrefs.SetFloat("Level0Time", 0);
        PlayerPrefs.SetInt("Level0Completed", 0);
        PlayerPrefs.SetFloat("Level1Parts", 0);
        PlayerPrefs.SetFloat("Level1Time", 0);
        PlayerPrefs.SetInt("Level1Completed", 0);
        PlayerPrefs.SetFloat("Level2Parts", 0);
        PlayerPrefs.SetFloat("Level2Time", 0);
        PlayerPrefs.SetInt("Level2Completed", 0);
    }
    public void Credits() {
       SceneManager.LoadScene("Credits");
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
        StartCoroutine(LevelManager.SwitchMusic(menumusic));
    }
    public void Continue() {
        SceneManager.LoadScene(PlayerPrefs.GetString("Current Level", "Tutorial"));
    }
    public void Settings() {
        SceneManager.LoadScene("Settings");
        //StartCoroutine(LevelManager.SwitchMusic(menumusic));
    }
    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
        //StartCoroutine(LevelManager.SwitchMusic(menumusic));
    }

    public void skipToTutorial() {
        SceneManager.LoadScene("Tutorial");
    }
}

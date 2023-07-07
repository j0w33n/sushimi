using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : EventTrigger {
    EventTrigger levelend;// Start is called before the first frame update
    public bool endlevel;
    public GameObject endscreen;
    public FadeIn fade;
    protected override void Start() {
        endlevel = false;
        fade = FindObjectOfType<FadeIn>();
        base.Start();
    }

    // Update is called once per frame
    void Update() {

    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>() && gameObject.tag == "Trigger") {
            StartCoroutine(LevelEnd());
        }
    }
    IEnumerator LevelEnd() {
        endlevel = true;
        AudioManager.instance.StopMusic();
        player.canMove = false;
        cam.followtarget = false;
        player.movement = Vector2.zero;
        AudioManager.instance.PlaySFX(AudioManager.instance.winSound);
        yield return new WaitForSeconds(AudioManager.instance.winSound.length - .1f);
        AudioManager.instance.StopSFX();
        endscreen.SetActive(true);
        endscreen.GetComponentInChildren<Text>().text = levelManager.timer.GetComponent<Text>().text;
    }
    public void EndScreen() {
        StartCoroutine(fade.Appear());
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

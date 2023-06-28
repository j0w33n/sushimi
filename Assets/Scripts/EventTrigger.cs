using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EventTrigger : MonoBehaviour
{
    public GameObject tutorialmsg,endscreen;
    public Player player;
    CameraController cam;
    public LevelManager levelManager;
    public FadeIn fade;
    public bool endlevel;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraController>();
        levelManager = FindObjectOfType<LevelManager>();
        fade = FindObjectOfType<FadeIn>();
        endscreen.SetActive(false);
        endlevel = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Player>() && gameObject.tag == "Trigger") {
            StartCoroutine(LevelEnd());
        }
        else if (collision.GetComponent<Player>()) {
            tutorialmsg.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            tutorialmsg.SetActive(false);
        }
    }
    IEnumerator LevelEnd() {
        endlevel = true;
        AudioManager.instance.StopMusic();
        player.canMove = false;
        cam.followtarget = false;
        player.movement = Vector2.zero;
        player.arrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        player.movement = new Vector2(1, player.movement.y) * player.rb.position.normalized;
        AudioManager.instance.PlaySFX(AudioManager.instance.winSound);
        yield return new WaitForSeconds(AudioManager.instance.winSound.length - .1f);
        AudioManager.instance.StopSFX();
        endscreen.SetActive(true);
        endscreen.GetComponentInChildren<Text>().text = levelManager.timer.GetComponent<Text>().text;
    }
}

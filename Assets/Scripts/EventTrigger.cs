using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventTrigger : MonoBehaviour
{
    public GameObject tutorialmsg;
    Player player;
    CameraController cam;
    LevelManager levelManager;
    FadeIn fade;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraController>();
        levelManager = FindObjectOfType<LevelManager>();
        fade = FindObjectOfType<FadeIn>();
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
        AudioManager.instance.StopMusic();
        player.canMove = false;
        cam.followtarget = false;
        player.movement = Vector2.zero;
        yield return new WaitForSeconds(1);
        player.movement = new Vector2(1, player.movement.y) * player.rb.position.normalized;
        yield return new WaitForSeconds(2);
        StartCoroutine(fade.Appear());
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(levelManager.leveltoload);
    }
}

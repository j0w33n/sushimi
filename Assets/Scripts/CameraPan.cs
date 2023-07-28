using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPan : EventTrigger {
    public MiniBossAI miniboss;
    public BossAI boss;
    // Start is called before the first frame update
    protected override void Start() {
        miniboss = FindObjectOfType<MiniBossAI>(true);
        boss = FindObjectOfType<BossAI>(true);
        base.Start();
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>() /*&& SceneManager.GetActiveScene().name == "Level 1"*/) {
            StartCoroutine(Pan(miniboss.transform));
        }
        else if(collision.GetComponent<Player>() && SceneManager.GetActiveScene().name == "Level 2") {
            StartCoroutine(Pan(boss.transform));
        }
    }
    // Update is called once per frame
    void Update() {

    }
    IEnumerator Pan(Transform target) {
        AudioManager.instance.StopMusic();
        player.canMove = false;
        cam.followtarget = false;
        player.movement = Vector2.zero;
        yield return new WaitForSeconds(2);
        target.gameObject.SetActive(true);
        target.GetComponent<Enemy>().healthbar.gameObject.SetActive(false);
        cam.followtarget = true;
        cam.target = target;
        yield return new WaitForSeconds(2);
        if(target.GetComponent<MiniBossAI>())StartCoroutine(LevelManager.SwitchMusic(AudioManager.instance.minibossmusic));
        else if(target.GetComponent<BossAI>()) StartCoroutine(LevelManager.SwitchMusic(AudioManager.instance.bossmusic));
        cam.target = player.transform;
        cam.GetComponent<Camera>().orthographicSize += 10;
        target.GetComponent<Enemy>().canMove = true;
        target.GetComponent<Enemy>().healthbar.gameObject.SetActive(true);
        player.canMove = true;
        boss.isShooting = true;
        player.respawnpoint = transform.position;
        gameObject.SetActive(false);
    }
}
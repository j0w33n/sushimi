using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : EventTrigger {
    public MiniBossAI miniboss;
    // Start is called before the first frame update
    protected override void Start() {
        miniboss = FindObjectOfType<MiniBossAI>(true);
        base.Start();
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            StartCoroutine(Pan(miniboss.transform));
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
        StartCoroutine(LevelManager.SwitchMusic(AudioManager.instance.minibossmusic));
        cam.target = player.transform;
        cam.GetComponent<Camera>().orthographicSize += 10;
        target.GetComponent<Enemy>().canMove = true;
        target.GetComponent<Enemy>().healthbar.gameObject.SetActive(true);
        player.canMove = true;
        player.respawnpoint = transform.position;
        gameObject.SetActive(false);
    }
}
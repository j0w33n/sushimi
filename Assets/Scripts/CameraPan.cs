using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : EventTrigger {
    public MiniBossAI miniboss;
    public Transform target;
    // Start is called before the first frame update
    void Start() {
        miniboss = FindObjectOfType<MiniBossAI>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            target = miniboss.transform;
            StartCoroutine(Pan());
        }
    }
    // Update is called once per frame
    void Update() {

    }
    IEnumerator Pan() {
        AudioManager.instance.StopMusic();
        player.canMove = false;
        cam.followtarget = false;
        player.movement = Vector2.zero;
        yield return new WaitForSeconds(2);
        cam.followtarget = true;
        cam.target = target;
        yield return new WaitForSeconds(2);
        cam.target = player.transform;
        player.canMove = true;
    }
}

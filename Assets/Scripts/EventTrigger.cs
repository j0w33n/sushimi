using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EventTrigger : MonoBehaviour {
    [SerializeField] protected GameObject tutorialmsg;
    public Player player;
    public CameraController cam;
    public LevelManager levelManager;
    // Start is called before the first frame update
    protected virtual void Start() {
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraController>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            tutorialmsg.SetActive(true);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            tutorialmsg.SetActive(false);
        }
    }
}

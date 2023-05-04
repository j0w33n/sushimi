using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed = 0.06f;
    public bool player;

    public GameObject button;


    void Start() {
        dialogueText.text = "";
    }

    void Update()
    { if(Input.GetKeyDown(KeyCode.F) && player) { // placeholder F key instead of touch controls for now
            if (dialoguePanel.activeInHierarchy) {
                text0();
            }
            else {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        
    }
    public void text0() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() {
        foreach(char letter in dialogue[index].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine() {
        if(index < dialogue.Length - 1) {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player = false;
            text0();
        }
    }
}

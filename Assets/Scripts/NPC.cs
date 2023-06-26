using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class NPC : MonoBehaviour {
    public string[] dialogueLines;
    public string[] dialogueLinesRaw;
    public Text dialogueText;
    public float typingSpeed = 0.01f;
    private int currentLineIndex = 0;
    private bool dialogueActive = false;
    public GameObject dialogueBox;
    Player player;
    public bool hasRead = false;
    public int hasTranslator = 0;

    private void Start() {
        dialogueBox.SetActive(false);
        player = FindObjectOfType<Player>();
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            Debug.Log("He's here");

            // If player clicks on NPC with 'E' key
            if (Input.GetKeyDown(KeyCode.E)) {
                StartDialogue();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            if (Input.GetKeyDown(KeyCode.E)) {
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            EndDialogue();
        }
    }

    private void Update() {
        hasTranslator = PlayerPrefs.GetInt("hasTranslator");
        if (dialogueActive && Input.GetKeyDown(KeyCode.Return)) {
            if (hasTranslator == 1) {
                ContinueDialogue();
            } else {
                ContinueDialogueRaw();
            }
            
        }
    }

    private void StartDialogue() {
        dialogueActive = true;
        hasRead = true;
        player.canMove = false;
        currentLineIndex = 0;
        ShowDialogueBox();
        if(hasTranslator == 1) {
            StartCoroutine(TypeOutDialogue());
        } else {
            StartCoroutine(TypeOutDialogueRaw());
        }
        
    }

    private void EndDialogue() {
        player.canMove = true;
        dialogueActive = false;
        HideDialogueBox();
    }

    private void ShowDialogueBox() {
        dialogueBox.SetActive(true);
    }

    private void HideDialogueBox() {
        dialogueBox.SetActive(false);
    }

    private IEnumerator TypeOutDialogue() {
        dialogueText.text = string.Empty;

        string currentLine = dialogueLines[currentLineIndex];
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char character in currentLine) {
            stringBuilder.Append(character);
            dialogueText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator TypeOutDialogueRaw() {
        dialogueText.text = string.Empty;

        string currentLine = dialogueLinesRaw[currentLineIndex];
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char character in currentLine) {
            stringBuilder.Append(character);
            dialogueText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ContinueDialogue() {
        if (dialogueText.text.Length >= dialogueLines[currentLineIndex].Length) {
            if (currentLineIndex < dialogueLines.Length - 1) {
                currentLineIndex++;
                StartCoroutine(TypeOutDialogue());
            } else {
                EndDialogue();
            }
        }
    }

    public void ContinueDialogueRaw() {
        if (dialogueText.text.Length >= dialogueLinesRaw[currentLineIndex].Length) {
            if (currentLineIndex < dialogueLinesRaw.Length - 1) {
                currentLineIndex++;
                StartCoroutine(TypeOutDialogue());
            } else {
                EndDialogue();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dialogue;

    void Start() {
        dialogue.SetActive(false);
    }
    void OnTriggerEnter2D(BoxCollider2D NPC) {
        dialogue.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

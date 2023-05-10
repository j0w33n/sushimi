using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject tutorialmsg;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            tutorialmsg.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            tutorialmsg.SetActive(false);
        }
    }
    
}

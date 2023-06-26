using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translator : MonoBehaviour
{
    public bool hasTranslator = false;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("hasTranslator") == 1) {
            hasTranslator = true;
        }
        
        if (hasTranslator) {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            Destroy(gameObject);
            PlayerPrefs.SetInt("hasTranslator", 1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

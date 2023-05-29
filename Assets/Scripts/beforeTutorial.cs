using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class beforeTutorial : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {

        StartCoroutine(LoadScene());

    }


    IEnumerator LoadScene() {
        yield return new WaitForSeconds(17);
        FindObjectOfType<SceneController>().skipToTutorial();
    }
}


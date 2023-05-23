using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public AudioClip menumusic;
    public float waittime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadScene() {
        yield return new WaitForSeconds(waittime);
        StartCoroutine(LevelManager.SwitchMusic(menumusic));
        SceneManager.LoadScene("MainMenu");
    }
}

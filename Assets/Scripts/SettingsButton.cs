using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{

    private Player thePlayer;
    private LevelManager theLevelManager;
    [SerializeField] GameObject soundMenu;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        theLevelManager = FindObjectOfType<LevelManager>();

    }

    public void Sound()
    {
        Time.timeScale = 0.0f;
        soundMenu.SetActive(true);
        thePlayer.canMove = false;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        soundMenu.SetActive(false);
        thePlayer.canMove = true;
    }
}


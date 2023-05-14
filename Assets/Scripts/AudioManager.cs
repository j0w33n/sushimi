using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;
    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
        LoadVolume();
    }
    void LoadVolume() {
        mixer.SetFloat("SFX Volume", Mathf.Log10(PlayerPrefs.GetFloat("SFX Volume",1f)) * 20);
        mixer.SetFloat("Music Volume", Mathf.Log10(PlayerPrefs.GetFloat("Music Volume", 1f)) * 20);
    }
    public void StopMusic() {
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void PlayMusic() {
        if (gameObject.GetComponent<AudioSource>().isPlaying) return;
        gameObject.GetComponent<AudioSource>().Play();
    }
}

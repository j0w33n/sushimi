using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;
    AudioSource audio;
    [SerializeField]AudioSource sfxaudio;
    public AudioClip partsound, healthsound,menumusic,waveclearsound,reloadsound,exitsound,entrancesound;
    private void Start() {
        audio = GetComponent<AudioSource>();
        PlayMusic(menumusic);
    }
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
        audio.Stop();
    }
    public void StopSFX() {
        sfxaudio.Stop();
    }
    public void PlayMusic(AudioClip music) {
        if (audio.isPlaying) return;
        audio.clip = music;
        audio.Play();
    }
    public void PlaySFX(AudioClip clip) {
        sfxaudio.PlayOneShot(clip);
    }
}

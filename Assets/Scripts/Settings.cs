using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    public Slider SFXSlider;

    private void Awake() {
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    void Start()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFX Volume", 1f);
        //audioMixer.SetFloat("volume", volume);
        //volumeSlider.value = volume;
    }
    public void SetSFXVolume(float volume)
    {
        // Set the AudioMixer volume to the slider value
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX Volume", SFXSlider.value);
        // Store the slider value in PlayerPrefs

    }

}

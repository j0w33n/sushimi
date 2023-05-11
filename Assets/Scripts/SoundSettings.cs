using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0f);
        audioMixer.SetFloat("volume", volume);
        volumeSlider.value = volume;
    }

    public void SetVolume(float volume)
    {
        // Set the AudioMixer volume to the slider value
        audioMixer.SetFloat("volume", volume);
        // Store the slider value in PlayerPrefs
        PlayerPrefs.SetFloat("volume", volume);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class EndText : MonoBehaviour
{
    public Text timetext;
    public Text parttext;
    public GameObject holder;
    public VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        holder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!video.isPlaying) holder.SetActive(true);
        timetext.text = PlayerPrefs.GetFloat("TotalTime").ToString();
        parttext.text = PlayerPrefs.GetFloat("TotalParts").ToString();
    }
}

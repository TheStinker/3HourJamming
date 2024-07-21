using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private double duration;
    private bool played;
    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();  
        duration = Time.time + videoPlayer.length;
    }
    private void Update()
    {
        if (Time.time >= duration && !played)
        {
            played = true;
            SceneManager.LoadScene("StartMenu");
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class PlayVideoInWebGL : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private string videoFileName;
  
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        Debug.Log(videoPlayer.url);
       // videoPlayer.Play();
    }
}
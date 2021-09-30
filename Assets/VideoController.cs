using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string VideoName;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, VideoName);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
   
            videoPlayer.Play();

        }
    }


    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
    
            videoPlayer.Stop();

        }
    }


}
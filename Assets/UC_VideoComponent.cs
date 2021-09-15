using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UC_VideoComponent : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer player;

    private bool isVideoStarted = false;

    public void StartVideo ()
    {
        string url = Application.streamingAssetsPath + "/IntroTest.mp4";
        if (File.Exists(url))
        {
            player.url = url;

            player.Play();
        }
        else
        {
            Invoke("VideoNotStartedCheck", 3);
        }
    }

    private void Update ()
    {
        if (player.isPlaying)
        {
            isVideoStarted = true;
        }

        if (isVideoStarted)
        {
            if (player.isPlaying == false)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void VideoNotStartedCheck ()
    {
        if (isVideoStarted == false)
        {
            gameObject.SetActive(false);
        }
    }
}

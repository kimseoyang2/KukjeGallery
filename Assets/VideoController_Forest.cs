using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController_Forest : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Screen;
    public string VideoName;
    public BGSoundManager BGSoundManager;
    public VideoController_Forest videoController_Forest;
    

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
           // videoController_Forest.enabled(true);
            Screen.SetActive(true);
            videoPlayer.Play();
            BGSoundManager.BgmSoundOnOff(false);
            

        }
    }
    

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            print("xxxx");
           
           videoController_Forest.enabled = false;
            BGSoundManager.BgmSoundOnOff(true);
            Screen.SetActive(false);
            videoPlayer.Stop();
        
        }
    }


}
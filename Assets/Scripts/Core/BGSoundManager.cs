using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGSoundManager : SingletonBehavior<BGSoundManager>
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip[] clips;

    public bool isOn;

    
    protected override void Awake ()
    {
        base.Awake();
     
    }
    private void Update()
    {
      if(audioSource.clip != clips[0])
        {            
            if(!audioSource.isPlaying)
            {
                ChangeClip(0);
            }
        }
    }
    public void ChangeClip(int clipIndex)
    {
        if(clips[clipIndex] == audioSource.clip)
        {
            clipIndex = 0;
        }

        AudioClip newClip;
        try
        {
            newClip = clips[clipIndex];
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log(string.Format("Clip[{0}] is not exist", clipIndex));
            return;
        }

        if(clipIndex == 0)
        {
            audioSource.loop = true;
        }else
        {
            audioSource.loop = false;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }

    public void BgmSoundOnOff(bool isOn)
    {
        if (isOn)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 0;
        }

    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

    //오디오 도슨트 플레이리스트
    // Picture 08,Picture01, Picture14


}

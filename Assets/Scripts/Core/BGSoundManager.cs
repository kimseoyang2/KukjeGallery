using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGSoundManager : SingletonBehavior<BGSoundManager>
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] clips;

    
    protected override void Awake ()
    {
        base.Awake();
    }
    private void Update()
    {
      
    }
    public void ChangeClip(int clipIndex)
    {
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

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

    public void ChangeClip(int clipIndex)
    {
        audioSource.Stop();
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }
}

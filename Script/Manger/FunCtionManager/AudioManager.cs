using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
///“Ù∆µπ‹¿Ì∆˜
/// </summary>

public class AudioManager : GlobalManagerBase<AudioManager>
{
    private AudioSource audioSource;
    private Coroutine playSoundEffectCoroutine;
    public override void Init()
    {
        base.Init();
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    public void PlaySoundEffect(AudioClip clip, float value=0.3F, float delayedTime = 0)
    {
        if(delayedTime==0) audioSource.PlayOneShot(clip, value);
        else
        playSoundEffectCoroutine = StartCoroutine(DOPlaySoundEffect(clip,value,delayedTime));
    }
    IEnumerator DOPlaySoundEffect(AudioClip clip, float value = 0.3F,float delayedTime=0)
    {
        yield return new WaitForSeconds(value);
        audioSource.PlayOneShot(clip, value);
       
    }   
    /// <summary>
    /// ≤•∑≈±≥æ∞“Ù¿÷
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="value"></param>
    public void PlayBgmSound(AudioClip clip, float value = 0.5F,bool loop=true)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = value;
        audioSource.Play();
    }
}

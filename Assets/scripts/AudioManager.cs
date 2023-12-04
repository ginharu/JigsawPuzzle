using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using MyUtils;

public class AudioManager : MonoBehaviour 
{
    private bool soundOpen;
    
    //声效
    public AudioSource soundAudio;
    public AudioSource bgmAudio;

    public static AudioManager instance;
    void Awake(){
        if(instance==null)instance=this;
        else if(instance!=this)Destroy(this.gameObject);
        SwitchBgmSound(ProgressData.Instance.AudioSettings.BgmIsOpen);

    }
    
    public void playSound(string name)
    {
        return;
        if (!ProgressData.Instance.AudioSettings.SoundIsOpen)
        {
            Debug.Log("SoundIsOpen = false,Play fail");
            return;
        }
        AudioClip sound = Resources.Load<AudioClip>($"Audio/{name}");
        soundAudio.PlayOneShot(sound);
    }
    
    public void SwitchBgmSound(bool IsOpen)
    {
        if (IsOpen)
        {
            bgmAudio.mute = false;
        }
        else
        {
            bgmAudio.mute = true;
        }
    }
    
    /// <summary>
    /// 播放一个声音
    /// </summary>
    /// <param name="name"></param>
    public static void Play(string name) {
        instance.playSound(name);
    }
    
    public void ButtonClick()
    {
        this.playSound("click");
    }
}
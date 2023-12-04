using System.Collections;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class Setting : MonoBehaviour
{

    public GameObject[] sound;
    public GameObject[] music;
    public GameObject[] vibrate;
    // Start is called before the first frame update
    void Awake()
    {

        if (ProgressData.Instance.AudioSettings.SoundIsOpen)
        {
            SoundClose();
        }
        else
        {
            SoundOpen();
        }
        
        
        if (ProgressData.Instance.AudioSettings.BgmIsOpen)
        {
            MusicClose();
        }
        else
        {
            MusicOpen();
        }
        
        if (ProgressData.Instance.AudioSettings.Vibrate)
        {
            VibrateClose();
        }
        else
        {
            VibrateOpen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundOpen()
    {
        sound[0].SetActive(false);
        sound[1].SetActive(true);
        ProgressData.Instance.AudioSettings.SoundIsOpen = false;
        AudioManager.instance.ButtonClick();
        JSONUtils.Save(ProgressData.Instance.AudioSettings);
    }
    
    public void SoundClose()
    {
        sound[1].SetActive(false);
        sound[0].SetActive(true);
        ProgressData.Instance.AudioSettings.SoundIsOpen = true;
        AudioManager.instance.ButtonClick();
        JSONUtils.Save(ProgressData.Instance.AudioSettings);
    }
    
    public void MusicOpen()
    {
        Debug.Log(222);
        music[0].SetActive(false);
        music[1].SetActive(true);
        AudioManager.instance.SwitchBgmSound(false);
        ProgressData.Instance.AudioSettings.BgmIsOpen = false;
        AudioManager.instance.ButtonClick();
        JSONUtils.Save(ProgressData.Instance.AudioSettings);
    }
    
    public void MusicClose()
    {
        Debug.Log(111);
        music[1].SetActive(false);
        music[0].SetActive(true);
        AudioManager.instance.SwitchBgmSound(true);
        ProgressData.Instance.AudioSettings.BgmIsOpen = true;
        AudioManager.instance.ButtonClick();
        JSONUtils.Save(ProgressData.Instance.AudioSettings);
    }
    
    public void VibrateOpen()
    {
        vibrate[0].SetActive(false);
        vibrate[1].SetActive(true);
        ProgressData.Instance.AudioSettings.Vibrate = false;
        AudioManager.instance.ButtonClick();
        JSONUtils.Save(ProgressData.Instance.AudioSettings);
    }
    
    public void VibrateClose()
    {
        vibrate[1].SetActive(false);
        vibrate[0].SetActive(true);
        ProgressData.Instance.AudioSettings.Vibrate = true;
        AudioManager.instance.ButtonClick();
        JSONUtils.Save(ProgressData.Instance.AudioSettings);
    }
}

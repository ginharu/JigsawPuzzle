
using System.Collections;
using Data;
using Script.Util;
using UnityEngine;

public class ProgressData
{
    public static ProgressData Instance { private set; get; } = null;
    public AudioSettings AudioSettings { private set; get; }
    
    public UserSaveData UserSaveData { set; get; }

    /// <summary>
    /// 是否执行完毕
    /// </summary>
    public bool IsDone { private set; get; } = false;


    
    
    public static IEnumerator Init()
    {
        // 如果未实例化，则开始实例化
        if (Instance == null)
        {
            Instance = new ProgressData();

            // //初始化用户数据
            UserSaveData userSaveData = JSONUtils.Load<UserSaveData>();
            if (userSaveData == null)
            {
                Debug.Log($"User Save Data is not found.");
                userSaveData = new UserSaveData();
                userSaveData.CurrentLevel = 0;
                userSaveData.NextLevel = 1;
            }
            Instance.UserSaveData = userSaveData;
            Debug.Log(Instance.UserSaveData.NextLevel);

            //
            AudioSettings audioSettings = JSONUtils.Load<AudioSettings>();
            if (audioSettings == null)
            {
                //初始化声音设置
                Instance.AudioSettings = new AudioSettings();
                Instance.AudioSettings.SoundIsOpen = true;
                Instance.AudioSettings.BgmIsOpen = true;
            }
            else
            {
                Debug.Log($"读取到声音配置:{JSONUtils.ToJSON(audioSettings)}");
                Instance.AudioSettings = audioSettings;
            }

            Instance.IsDone = true;
            // 初始化成功
            Debug.Log($"初始化数据成功");

        }
        else
        {
            Instance.IsDone = true;
        }
        yield break;
    }
}

public class AudioSettings
{
    /// <summary>
    /// 是否打开背景音乐
    /// </summary>
    public bool BgmIsOpen = true;
    /// <summary>
    /// 是否打开音效
    /// </summary>
    public bool SoundIsOpen = true;
    /// <summary>
    /// 是否打开震动
    /// </summary>
    public bool Vibrate = true;
}
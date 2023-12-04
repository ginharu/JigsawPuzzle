using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{

    public GameObject Setting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingOn()
    {
        AudioManager.instance.ButtonClick();
        Setting.SetActive(true);
    }

    public void SettingOff()
    {
        AudioManager.instance.ButtonClick();
        Setting.SetActive(false);
    }

}

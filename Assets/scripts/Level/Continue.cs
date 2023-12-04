using System.Collections;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public GameObject Exit;
    public GameObject Tip;
    public GameObject Img;
    public GameObject Bottom;
    
    // Start is called before the first frame update
    void Awake()
    {
        Exit.SetActive(false);
        Tip.SetActive(false);
        Img.SetActive(false);
        Bottom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        ProgressData.Instance.UserSaveData.NextLevel += 1;
        JSONUtils.Save(ProgressData.Instance.UserSaveData);
        SceneManager.LoadSceneAsync("Scenes/Home");

    }
    
}

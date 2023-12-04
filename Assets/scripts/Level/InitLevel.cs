using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitLevel : MonoBehaviour
{

    public GameObject Level; 
    public Sprite[] ActiveList; 
    public Sprite[] HiddenList;

    public Button btn;

    // Start is called before the first frame update
    void Awake()
    {
        LoadImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLevel()
    {
        Instantiate(Level, transform);
    }
    
    public void LoadImage()
    {
        for (int i = 0; i < HiddenList.Length; i++)
        {
            Instantiate(Level, transform);
        }

        LoadLevel[] levels = GetComponentsInChildren<LoadLevel>();
        Debug.Log(ProgressData.Instance.UserSaveData.NextLevel);
        Debug.Log(333333333333333333);
        for (int i = 0; i < levels.Length; i++)
        {
            if (i+1 < ProgressData.Instance.UserSaveData.NextLevel)
            {
                levels[i].UpdateImage(ActiveList[i], ""+(i+1));
                if (ProgressData.Instance.UserSaveData.CurrentLevel==0)
                {
                    levels[i].Next.SetActive(true);
                    levels[i].UpdateImage(HiddenList[i], ""+(i+1));
                }
            }else if (i+1 == ProgressData.Instance.UserSaveData.NextLevel)
            {
                levels[i].Next.SetActive(true);
                levels[i].UpdateImage(HiddenList[i], ""+(i+1));
            }
            else
            {
                levels[i].UpdateImage(HiddenList[i], ""+(i+1));
                levels[i].Lock.SetActive(true);
            }

        }
    }

    
}

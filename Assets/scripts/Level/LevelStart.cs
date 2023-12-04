using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{

    public LevelManager[] levels;
    public GameObject ActiveLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        levels = GetComponentsInChildren<LevelManager>();
        for (int i = 0; i < levels.Length; i++)
        {
           // if (ProgressData.Instance.UserSaveData.CurrentLevel == i + 1)
            if (true)
            {
                levels[i].gameObject.SetActive(true);
                ActiveLevel = levels[i].gameObject;
                Debug.Log("1111111111111111111111111");
            }
            else
            {
                levels[i].gameObject.SetActive(false);

            }
            
        }
        foreach (var level in levels)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    
    public Image LevelImage;
    public GameObject Lock;
    public GameObject Next;
    public TMP_Text LevelNumber;
    public int _levelNumber;

    public Button btn;
    // Start is called before the first frame update
    void Awake()
    {
        btn.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateImage(Sprite img, string levelNumber)
    {
        LevelImage.sprite = img;
        LevelNumber.text = levelNumber;
        _levelNumber = int.Parse(levelNumber);
        if (int.Parse(levelNumber) > ProgressData.Instance.UserSaveData.NextLevel)
        {
            btn.interactable = false;
        }
    }
    
    
    public void startGame()
    {
        AudioManager.instance.ButtonClick();
        Debug.Log(ProgressData.Instance.UserSaveData.NextLevel);
        if (_levelNumber <= ProgressData.Instance.UserSaveData.NextLevel)
        {
            ProgressData.Instance.UserSaveData.CurrentLevel = _levelNumber;
            ProgressData.Instance.UserSaveData.TipNumber = 1;
            SceneManager.LoadSceneAsync("Scenes/Game");
            
        }
    }
}

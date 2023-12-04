using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShapeInit : MonoBehaviour
{
    
    public Image imagePrefab; // 图片预制体
    
    public bool InitFinish;
    public TMP_Text Remaining;
    
        
    private int _LevelNumber;
    
    private RectTransform moveTransform1;
    // Start is called before the first frame update
    void Start()
    {
        _LevelNumber = ProgressData.Instance.UserSaveData.CurrentLevel;
        //_LevelNumber =9;
        // 根据对象名称查找物体
        GameObject canvasRs = GameObject.Find("Canvas");
        if (canvasRs != null)
        {
            RectTransform moveTransform = canvasRs.GetComponent<RectTransform>();
            moveTransform1 = moveTransform;
        }
        ReBuildShapes();
        InitFinish = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    public void ReBuildShapes() {
        NewShapes();
    }

    public void UpdateNumber()
    {
        ShapeDraggable[] shapeList = GetComponentsInChildren<ShapeDraggable>();
        Remaining.text = "1/" + shapeList.Length;
    }
    private void NewShapes()
    {
        //LoadImagesFromResources(ProgressData.Instance.UserSaveData.NextLevel);
        LoadImagesFromResources(_LevelNumber);
    }
    
    
    private void LoadImagesFromResources(int levelNumber)
    {
        Sprite[] imageSprites = Resources.LoadAll<Sprite>($"Level/level{levelNumber}");
        ShuffleArray(imageSprites);
        Debug.Log(imageSprites.Length);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(423*imageSprites.Length, rectTransform.sizeDelta.y);

        for (int i = 0; i < imageSprites.Length; i++)
        {
            CreateImageObject(imageSprites[i], i);
        }
    }
    
    private void CreateImageObject(Sprite sprite, int idx)
    {
        Image image = Instantiate(imagePrefab, transform);
        image.sprite = sprite;
        image.GetComponent<ShapeDraggable>().UpdateImageIdx(idx);
        image.GetComponent<ShapeDraggable>().SetOsCanvas(moveTransform1);
        if (_LevelNumber >= 9)
        {
            image.name = "sketch (" + image.sprite.name+")";
        }
        else
        {
            image.name = "sketch_" + image.sprite.name;
        }
    }
    
    private void ShuffleArray<T>(T[] list)
    {
        int n = list.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameItem : MonoBehaviour
{
    public enum TtemType
    {
        Item = 0,
        Sketch = 1
    }
    
    
    public RectTransform canvasRectTransform; // 用于获取Canvas的RectTransform
    public GameObject Content;
    
    [HideInInspector]
    public string LayerNumber;
    
    private Transform childTransform;
    private Vector2 canvasPosition;
    
    
    public event Action<string, bool> LayerUpdate;

    public TtemType ImageType;
    
    void Start()
    {
        if (ImageType == TtemType.Item)
        {
            return;
        }
        RectTransform objectRectTransform = GetComponent<RectTransform>();
        
        // 检查Canvas和物体的RectTransform是否已分配
        if (canvasRectTransform != null && objectRectTransform != null)
        {
            // 获取物体的世界坐标
            Vector3 worldPosition = objectRectTransform.position;

            // 将世界坐标转换为Canvas中的坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, worldPosition, Camera.main, out canvasPosition);

            Debug.Log("Canvas World Position: " + canvasPosition +" " +transform.name);
        }
        else
        {
            Debug.LogError("CanvasRectTransform or ObjectRectTransform not assigned.");
        }
    }

    
    void FixedUpdate()
    {
        if (ImageType == TtemType.Item)
        {
            return;
        }
        if (Content.GetComponent<ShapeInit>().InitFinish && childTransform == null)
        {
            Debug.Log(name);
            Transform cTransform = Content.transform.Find(name);

            childTransform = cTransform;
        }    
        
        Vector2 childCanvasPosition;
        if (childTransform == null)
        {
            Debug.Log($"transfrom is null name:{name}");
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, childTransform.position, Camera.main, out childCanvasPosition);
        if (transform.name == "sketch_cloud")
        {
//            Debug.Log("Canvas World Position: " + childCanvasPosition +" " +transform.name);
        }
        if (Mathf.Abs(canvasPosition.x - childCanvasPosition.x) <=1f && Mathf.Abs(canvasPosition.y - childCanvasPosition.y) <=1f && !childTransform.GetComponent<ShapeDraggable>().finish)
        {
            AudioManager.instance.playSound("common_done");
            Debug.Log("目标重合");
            UpdateImage();
            ShapeInit shape = Content.GetComponent<ShapeInit>();
            shape.UpdateNumber();
        }
    }

    private void UpdateImage()
    {
        // 更新对应图片框
        Image img = GetComponent<Image>();
        Color newColor = img.color;
        newColor.a = 0; // 将透明度设置为0
        img.color = newColor;
        
        Transform mgObj = null;

        if (ProgressData.Instance.UserSaveData.NextLevel >= 9)
        {
            // 寻找左括号的索引
            int leftParenthesisIndex = name.IndexOf('(');

            // 寻找右括号的索引
            int rightParenthesisIndex = name.IndexOf(')');
            string numberString = name.Substring(leftParenthesisIndex + 1, rightParenthesisIndex - leftParenthesisIndex - 1);

             mgObj = transform.parent.Find("item (" + numberString+")");
        }
        else
        {
            // 更新背景
            char[] delimiters = { '_' }; // 以逗号作为分隔符
            string[] substrings = name.Split(delimiters);
            mgObj = transform.parent.Find("item_" + substrings[1]);
        }
        Image imgBg = mgObj.GetComponent<Image>();
        Color newBGColor = imgBg.color;
        newBGColor.a = 255; // 将透明度设置为0
        imgBg.color = newBGColor;
        
        // 销毁物体
        ShapeDraggable obj = childTransform.GetComponent<ShapeDraggable>();
        obj.SimulateDragEnd();
        Debug.Log(obj.transform.parent.GetChild(0).name);
        ScrollRect scroll = obj.transform.parent.parent.GetComponent<ScrollRect>();
        scroll.horizontal = true;

        LayerUpdate?.Invoke(LayerNumber, true);
        
        Image[] imageSprites = Content.GetComponentsInChildren<Image>();
        RectTransform rectTransform = Content.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(423*imageSprites.Length, rectTransform.sizeDelta.y);
    }

    public void UpdateColorA()
    {
        Image imgBg = GetComponent<Image>();
        Color newBGColor = imgBg.color;
        newBGColor.a = 0; // 将透明度设置为0
        imgBg.color = newBGColor;
    }
}


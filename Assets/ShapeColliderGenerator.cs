using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShapeColliderGenerator : MonoBehaviour
{
    private RectTransform canvasRectTransform; // 用于获取Canvas的RectTransform
    private RectTransform objectRectTransform; // 用于获取物体的RectTransform

    private void Start()
    {
        objectRectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        // 检查Canvas和物体的RectTransform是否已分配
        if (canvasRectTransform != null && objectRectTransform != null)
        {
            // 获取物体的世界坐标
            Vector3 worldPosition = objectRectTransform.position;

            // 将世界坐标转换为Canvas中的坐标
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, worldPosition, Camera.main, out canvasPosition);

            Debug.Log("Canvas World Position1111: " + canvasPosition);
        }
        else
        {
            Debug.LogError("CanvasRectTransform or ObjectRectTransform not assigned.");
        }
    }
}

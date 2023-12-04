using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 在拖拽开始时的操作（可选）
        //gameObject.SetActive(false);
        weight.Instance.CurrentObj1 = gameObject;
        Debug.Log(1111);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Image img = GetComponent<Image>();
        img.SetNativeSize();
        Vector2 screenPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out screenPos))
        {
            rectTransform.anchoredPosition = screenPos;
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        // 在拖拽开始时的操作（可选）
        //weight.Instance.CurrentObj1.SetActive(true);
        Debug.Log(2222);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShapeDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 选中后的缩放比率
    /// </summary>
    private Vector3 shapeSelectedScale;
    
    /// <summary>
    /// 形状起始缩放比率
    /// </summary>
    private Vector3 currentShapeScale;

    /// <summary>
    /// 移动
    /// </summary>
    private RectTransform moveTransform;
    
    /// <summary>
    /// 起始位置
    /// </summary>
    private Vector3 startPosition;

    // 绘制
    private RectTransform parent;

    /// <summary>
    /// 是否能被拖拽
    /// </summary>
    private bool canBeDragged = true;
    
    private bool dragIng = false;

    private float offsetY = 0;

    private float canvasScaleY = 0;
    
    private Image image;

    private int idx;

    private RectTransform canvasRectTransform;

    public bool finish;

    private Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        moveTransform = GetComponent<RectTransform>();
        _canvas = GetComponent<Canvas>();
        parent = transform.parent.GetComponent<RectTransform>();
        image = GetComponent<Image>();
        ImageZoom();
        // ShapeData shapeData = GetComponent<Shape>().ShapeData;
        // offsetY = (square.rect.height  * (shapeData.Rows / 4f + 0.5f) + (shapeData.Rows - 1) * Grid.Instance.GetComponent<GridLayoutGroup>().spacing.y) * shapeSelectedScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canvasRectTransform != null && moveTransform != null && dragIng)
        {
            // 获取物体的世界坐标
            Vector3 worldPosition = moveTransform.position;

            // 将世界坐标转换为Canvas中的坐标
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, worldPosition, Camera.main, out canvasPosition);

            // Debug.Log("Canvas World: " + canvasPosition);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 拖动时滑块禁止移动
        SetUnDraggable(false);
        AudioManager.instance.ButtonClick();
        
        Debug.Log(idx);
        if (!canBeDragged) {
            Debug.Log("Current Game is Paused.");
            return;
        }
        currentShapeScale = moveTransform.localScale;
        moveTransform.localScale = shapeSelectedScale;
        Image img = GetComponent<Image>();
        img.SetNativeSize();
        parent = transform.parent.parent.GetComponent<RectTransform>();;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {

        _canvas.overrideSorting = true;
        _canvas.sortingOrder = 3;
        if (!canBeDragged)
        {
            Debug.Log("Current Game is Paused.");
            return;
        }

        transform.localScale = Vector3.one;
        Vector2 pos;
        //计算拖转位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, eventData.position + new Vector2(0, offsetY) * canvasScaleY, Camera.main, out pos);
        //设置位置
        moveTransform.localPosition = pos;
        dragIng = true;
    }
    
    
    public void OnEndDrag(PointerEventData eventData)
    {
        // 拖动时滑块禁止移动

        _canvas.overrideSorting = false;
        if (finish)
        {
            return;
        }
        ImageZoom();
        if (!canBeDragged)
        {
            Debug.Log("Current Game is Paused.");
            return;
        }
        moveTransform.localScale = currentShapeScale;
        Vector3 pos;
        //计算世界坐标
        RectTransformUtility.ScreenPointToWorldPointInRectangle(parent, eventData.position + new Vector2(0, offsetY) * canvasScaleY, Camera.main, out pos);

        var newParent = transform.parent;
        parent = newParent.GetChild(0).GetComponent<RectTransform>();;
        transform.SetParent(newParent.GetChild(0));
        transform.SetSiblingIndex(idx);
        SetUnDraggable(true);
        dragIng = false;
    }

    private void ImageZoom()
    {
        // 图片大小自适应变化
        image.SetNativeSize();
        
        RectTransform rect = image.GetComponent<RectTransform>();

        float width = 200/image.GetComponent<RectTransform>().rect.height * image.GetComponent<RectTransform>().rect.width;
        
        rect.sizeDelta = new Vector2(width, 200);
    }

    public void UpdateImageIdx(int index)
    {
        idx = index;
    }
    
    public void SetOsCanvas(RectTransform canvasTs)
    {
        canvasRectTransform = canvasTs;
    }

    public void DestroyObj()
    {
        Destroy(this);
    }

    public void SetFinish(bool isfinish)
    {
        finish = isfinish;
    }
    public void SimulateDragEnd()
    {
        finish = true;
        // 手动触发拖动结束事件
        ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.endDragHandler);
        // 更新子物体索引
        Transform aa = transform.parent.GetChild(0);
        ShapeDraggable[] ts = aa.GetComponentsInChildren<ShapeDraggable>();
        for (int i = 0; i < ts.Length; i++)
        {
            Debug.Log(ts[i].name);
            ts[i].UpdateImageIdx(i);
        }
        gameObject.SetActive(false);
    }

    public void SetUnDraggable(bool isUnDraggable)
    {
        ScrollRect scroll = transform.parent.parent.parent.GetComponent<ScrollRect>();
        scroll.horizontal = isUnDraggable;
    }
}

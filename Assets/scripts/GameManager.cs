using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Sprite[] GuideImg;
    public GameObject GuideImg2;

    public GameObject Content;
    public GameObject LevelControl;
    public ScrollRect  ScrollRect ;
    public GameObject  Guide ;
    public RectTransform canvasRectTransform; // 用于获取Canvas的RectTransform
    private Vector2 canvasPosition;
    
    public Transform targetPointB;
    public float speed = 5000f;
    private bool isMoving = false;
    private bool Guideing;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Guideing && Input.GetMouseButtonDown(0))
        {
            Guideing = false;
            isMoving = false;
            Guide.SetActive(false);
        }
    }
    

    public void Exit()
    {
        ProgressData.Instance.UserSaveData.CurrentLevel = ProgressData.Instance.UserSaveData.NextLevel -1;
        SceneManager.LoadSceneAsync("Scenes/Home");
    }


    public void Tip()
    {
        Guideing = true;
        // 获取全部元素
        ShapeDraggable[] shaeps = Content.GetComponentsInChildren<ShapeDraggable>();

        LevelStart level = LevelControl.GetComponent<LevelStart>();
        LevelManager[] levelM = level.ActiveLevel.GetComponentsInChildren<LevelManager>();
        foreach (var manager in levelM)
        {
            if (manager.gameObject.activeSelf)
            {
                GameItem[] items = manager.GetComponentsInChildren<GameItem>();
                foreach (var item in items)
                {
                    Image itemImg = item.GetComponent<Image>();
                    if (item.ImageType == GameItem.TtemType.Sketch && itemImg.color.a != 0)
                    {
                        for (int i = 0; i < shaeps.Length; i++)
                        {
                            // 找到对应的物体
                            if (shaeps[i].name == item.name)
                            {
                                StartCoroutine(MoveMove(shaeps[i].gameObject, items, item));
                            }
                            
                        }
                        break;
                    }
                }
            }
        }
        // 获取当前拼图图层
    }


    private IEnumerator MoveMove(GameObject shape, GameItem[] items, GameItem item)
    {
        while (Guideing)
        {
            Guide.GetComponent<Image>().sprite = GuideImg[0];
            RectTransform reTrans = shape.GetComponent<RectTransform>();
            // 获取目标元素的 anchoredPosition
            Vector2 targetPosition = reTrans.anchoredPosition;
            ScrollRect.vertical = false;
            // 设置滚动视图的 anchoredPosition，将目标元素滚动到视图中央
            ScrollRect.content.anchoredPosition =
                new Vector2(-targetPosition.x + 400, ScrollRect.content.anchoredPosition.y);


            RectTransform objectRectTransform = Guide.GetComponent<RectTransform>();
            Vector2 objectAPositionInCanvas = GetCanvasPosition(reTrans.position);

            MoveObjectToPosition(objectRectTransform, objectAPositionInCanvas);
            Guide.SetActive(true);

            RectTransform targetB = GetTargetPosition(items, item.name);

            Debug.Log(canvasPosition.x);
            yield return StartCoroutine(replaceImg());
            yield return StartCoroutine(StartMoving(objectRectTransform, targetB));
            yield return new WaitUntil(() => isMoving==false);
        }
    }
    
    
    
    
    
    
    
    
    private Vector2 GetCanvasPosition(Vector3 worldPosition)
    {
        Vector2 canvasPosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, worldPosition, null, out canvasPosition);
        return canvasPosition;
    }
    
    private void MoveObjectToPosition(RectTransform targetTransform, Vector2 targetPosition)
    {
        targetTransform.anchoredPosition = new Vector2(targetPosition.x+200, targetPosition.y-100);
    }


    private IEnumerator replaceImg()
    {
        yield return new WaitForSeconds(0.4f);
       Image guideImg =  Guide.GetComponent<Image>();
       guideImg.sprite = GuideImg[1];
    }
    
    
    IEnumerator StartMoving(RectTransform from, RectTransform targetPointB)
    {
        yield return new WaitForSeconds(0.3f);

        isMoving = true;

        Vector2 startPosition = from.anchoredPosition;
        Debug.Log(startPosition);
        Debug.Log(targetPointB.anchoredPosition);
        Vector2 newTargetPosition = new Vector2(targetPointB.anchoredPosition.x+100, targetPointB.anchoredPosition.y);
        float journeyLength = Vector2.Distance(startPosition,newTargetPosition);
        float startTime = Time.time;

        while (isMoving)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            from.anchoredPosition = Vector2.Lerp(startPosition,newTargetPosition, fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                isMoving = false;
                Debug.Log("UI Object has reached the target!");
            }

            yield return null;
        }
    }

    // 找到目标物体
    private RectTransform GetTargetPosition(GameItem[] items, string name)
    {
        string[] result = name.Split('_');

        string itemName = "item_" + result[1];
        foreach (var tItem in items)
        {
            Debug.Log(tItem.name);
            Debug.Log(itemName);
            if (tItem.name == itemName)
            {
                return tItem.GetComponent<RectTransform>();
            }
        }

        return null;
    }
}

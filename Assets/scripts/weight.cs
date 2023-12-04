using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weight : MonoBehaviour
{
    public static weight Instance;
    private Image[] splinters;

    public GameObject CurrentObj1;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Randoms();
        Image[] ts = GetComponentsInChildren<Image>();
        splinters = ts;
        foreach (var image in ts)
        {
            image.SetNativeSize();
            Debug.Log(image.GetComponent<RectTransform>().rect.width);
            //Debug.Log();
            RectTransform rect = image.GetComponent<RectTransform>();

            float width = 150/image.GetComponent<RectTransform>().rect.height * image.GetComponent<RectTransform>().rect.width;
            //
            rect.sizeDelta = new Vector2(width, 150);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Randoms()
    {
        Transform[] childObjects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i);
        }

        // 随机排列子物体的顺序
        for (int i = 0; i < childObjects.Length; i++)
        {
            int randomIndex = Random.Range(i, childObjects.Length);
            (childObjects[i], childObjects[randomIndex]) = (childObjects[randomIndex], childObjects[i]);
        }

        // 重新设置子物体的顺序
        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i].SetSiblingIndex(i);
        }
    }


    public void CurrentObj()
    {
        
    }
    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] layers;

    public GameObject Finish;
    
    public event Action<int, int> LayerUpdate;

    private Dictionary<string, int> layerCount;

    public GameObject CurrentLayer;
    // Start is called before the first frame update
    void Awake()
    {
        SRDebug.Init();
        layerCount = new Dictionary<string, int>();

        for (int i = 0; i < layers.Length; i++)
        {
            if (i == 0)
            {
                layers[i].SetActive(true);
                CurrentLayer =  layers[i];
                UpdateTtemImageA(layers[i]);

            }
            else
            {
                layers[i].SetActive(false);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            GameItem[] items = layers[i].GetComponentsInChildren<GameItem>();
            string result = layers[i].name.Substring("Layer".Length);
            int itemsnumber = 0;
            foreach (var item in items)
            {
                if (item.ImageType == GameItem.TtemType.Sketch)
                {
                    itemsnumber++;
                }
                item.LayerUpdate += UpdateLayer;
                item.LayerNumber = result;
            }
            //layerCount.Add(result, items.Length);
            layerCount[result] = itemsnumber;
        }
    }

    private IEnumerator GetLayerNumbers()
    {

        yield return null;
    }
    private void UpdateLayer(string layerNumber, bool finish)
    {
        Debug.Log(layerCount[layerNumber]);
        layerCount[layerNumber] -= 1;
        if (layerCount[layerNumber] ==0)
        {
            if (layers.Length == int.Parse(layerNumber) +1)
            {
                Debug.Log("success, 完成全部拼接");
                Finish.SetActive(true);
                return;
            }
            // 该场景全部完成，进行处理
            layers[int.Parse(layerNumber) +1].SetActive(true);
            CurrentLayer = layers[int.Parse(layerNumber) + 1];
            UpdateTtemImageA(layers[int.Parse(layerNumber) + 1]);
        }
    }

    private void UpdateTtemImageA(GameObject layer)
    {
        GameItem[] Items = layer.GetComponentsInChildren<GameItem>();
        foreach (var item in Items)
        {
            if (item.ImageType == GameItem.TtemType.Item)
            {
                item.UpdateColorA();
            }
        }
    }

}

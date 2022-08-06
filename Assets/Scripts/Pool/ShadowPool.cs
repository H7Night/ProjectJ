using UnityEngine;
using System.Collections.Generic;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;
    public GameObject shadowPrefab;
    public int shadowCount;
    Queue<GameObject> avaliableObject = new Queue<GameObject>();
    void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        FillPool();//初始化对象池
    }
    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            ReturnPool(newShadow);//返回对象池
        }
    }
    public GameObject GetFormPool()
    {
        if(avaliableObject.Count == 0)
        {
            FillPool();
        }
        var outShadow = avaliableObject.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        avaliableObject.Enqueue(gameObject);
    }
}

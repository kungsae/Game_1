using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager<T> where T : MonoBehaviour
{
    static private PoolManager<T> Instance;
    static public PoolManager<T> instance
    { 
        get 
        {
            if (Instance == null)
                Instance = new PoolManager<T>();
            return Instance;
        } 
    }
    Dictionary<string, Stack<T>> pools = new Dictionary<string, Stack<T>>();
    GameObject parent;
    Spawner spawner;
    public PoolManager()
    {
        parent = new GameObject(typeof(T) + " Pool");
        parent.AddComponent(typeof(Spawner));
        spawner = parent.GetComponent<Spawner>();
    }
    public T GetPool(GameObject obj)
    {
        if (!pools.ContainsKey(obj.gameObject.name))
        {
            pools.Add(obj.gameObject.name, new Stack<T>());
        }
        if (pools[obj.gameObject.name].Count == 0)
        {
            GameObject newObj = Instance.spawner.Instantiation(obj, parent);
            newObj.SetActive(false);
            return newObj.GetComponent<T>();
        }
        return pools[obj.gameObject.name].Pop();
    }
    public void SetPool(T obj)
    {
        if (!pools.ContainsKey(obj.gameObject.name))
        {
            pools.Add(obj.gameObject.name, new Stack<T>());
            obj.transform.parent = parent.transform;
        }
        pools[obj.gameObject.name].Push(obj);
        obj.gameObject.SetActive(false);
    }
}

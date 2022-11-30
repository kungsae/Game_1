using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager<T> where T : Component
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
    Dictionary<string, Queue<T>> pools = new Dictionary<string, Queue<T>>();
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
        if (!pools.ContainsKey(obj.gameObject.name+ "(Clone)"))
        {
            pools.Add(obj.gameObject.name+ "(Clone)", new Queue<T>());
        }
        if (pools[obj.gameObject.name+ "(Clone)"].Count == 0)
        {
            GameObject newObj = Instance.spawner.Instantiation(obj, parent);
            newObj.SetActive(false);
            return newObj.GetComponent<T>();
        }
        Debug.Log(pools[obj.gameObject.name + "(Clone)"].Count + obj.name);
        return pools[obj.gameObject.name + "(Clone)"].Dequeue();
    }
    public void SetPool(T obj)
    {
        if (!pools.ContainsKey(obj.gameObject.name))
        {
            pools.Add(obj.gameObject.name, new Queue<T>());
            obj.transform.parent = parent.transform;
        }
        obj.gameObject.SetActive(false);
        pools[obj.gameObject.name].Enqueue(obj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Edwon.Tools;

public class Pool : MonoBehaviour
{
    public PoolSettingsSO settings;
    public List<IPoolable> pool;
    Transform poolParent;
    
    [Header("Debug")]
    public bool unfoldInHierarchy = false;

    void Awake()
    {
        poolParent = this.transform;
        InitializePool();
        if (unfoldInHierarchy)
            Utils.UnfoldInEditorHierarchy(poolParent);
    }

    void InitializePool()
    {
        pool = new List<IPoolable>();
        foreach(PoolSettingsSO.Prefab prefab in settings.prefabs)
        {
            for (int i = 0; i < prefab.numberInPool; i++)
            {
                GameObject spawned = GameObject.Instantiate(prefab.prefab, Vector3.zero, Quaternion.identity);
                spawned.transform.parent = poolParent;
                spawned.name = prefab.poolableType;
                IPoolable poolable = spawned.GetComponent<IPoolable>();
                pool.Add(poolable);
                spawned.SetActive(false);
            }
        }
    }

    public IPoolable RemoveFromPool(string poolableType)
    {
        IPoolable poolable = null;
        for(int i = 0; i < pool.Count; i++)
        {
            if (pool[i].PoolableType == poolableType)
            {
                poolable = pool[i];
                poolable.GameObject.SetActive(true);
                poolable.OnUnPooled();
                pool.Remove(poolable);
            }
            else
            {
                Debug.LogWarning("no poolable of type " + poolableType + " found in pool");
            }
        }
        return poolable;
    }
    
    void ReturnToPool(IPoolable poolable)
    {
        poolable.OnPooled();
        poolable.GameObject.transform.position = Vector3.zero;
        poolable.GameObject.transform.rotation = Quaternion.identity;
        poolable.GameObject.transform.parent = poolParent;
        poolable.GameObject.SetActive(false);
        pool.Add(poolable);
    }
    
    public void ReturnAllToPool()
    {
        for (int i = pool.Count-1; i >= 0; i--)
        {
            ReturnToPool(pool[i]);
        }
    }
}

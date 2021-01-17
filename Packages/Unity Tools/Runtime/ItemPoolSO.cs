using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Item Pool SO", menuName = "Scriptables/Item Pool SO")]
    public class ItemPoolSO : ScriptableObject
    {
        public bool debugLog = false;
        public ItemStorageSO itemStorage;
        public List<Item> pool;
        public List<Item> active;
        [HideInInspector]
        public ItemPoolMB itemPoolMB;

        public Item SpawnFromPool(string itemName, Vector3 position, Quaternion rotation)
        {
            Item spawned = SpawnFromPool(itemName);
            spawned.transform.position = position;
            spawned.transform.rotation = rotation;
            return spawned;
        }

        public Item SpawnFromPool(string itemName)
        {
            if (debugLog)
                Debug.Log("try to spawn " + itemName + " from item pool");

            Item item = null;
            for(int i = 0; i < pool.Count; i++)
            {
                if (pool[i].itemName == itemName)
                {
                    item = pool[i];
                    item.gameObject.SetActive(true);
                    item.OnUnPooled();
                    pool.Remove(item);
                    active.Add(item);
                    item.transform.parent = null;
                    break;
                }
            }
            if(item == null) {Debug.LogWarning("no item of type " + itemName + " found in item pool");};

            return item;
        }
        
        public void ReturnToPool(Item item)
        {
            if (debugLog)
                Debug.Log("return " + item.gameObject.name + " to pool");

            item.OnPooled();
            item.gameObject.transform.position = Vector3.zero;
            item.gameObject.transform.rotation = Quaternion.identity;
            item.gameObject.transform.parent = itemPoolMB.poolParent;
            item.gameObject.SetActive(false);
            active.Remove(item);
            pool.Add(item);
        }
        
        public void ReturnAllToPool()
        {
            if (debugLog)
                Debug.Log("return all to pool");

            for (int i = active.Count-1; i >= 0; i--)
            {
                ReturnToPool(active[i]);
            }
        }

        public void ClearPool()
        {
            if (debugLog)
                Debug.Log("clear pool");

            pool.Clear();
        }
    }
}
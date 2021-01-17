using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        public void InitializePool()
        {
            itemDistanceComparer = new ItemDistanceComparer();
            pool = new List<Item>();
            active = new List<Item>();
            foreach(ItemStorageSO.ItemSlot slot in itemStorage.itemSlots)
            {
                for (int i = 0; i < slot.numberInPool; i++)
                {
                    GameObject spawned = GameObject.Instantiate(slot.itemPrefab.gameObject, Vector3.zero, Quaternion.identity);
                    spawned.transform.parent = itemPoolMB.poolParent;
                    spawned.name = slot.itemPrefab.itemName;
                    Item item = spawned.GetComponent<Item>();
                    pool.Add(item);
                    spawned.SetActive(false);
                }
            }
        }

        public Item SpawnFromPool(string itemName, Vector3 position, Quaternion rotation)
        {
            if (debugLog)
                Debug.Log("try to spawn " + itemName + " from item pool");

            Item item = null;
            for(int i = 0; i < pool.Count; i++)
            {
                if (pool[i].itemName == itemName)
                {
                    item = pool[i];
                    item.transform.position = position;
                    item.transform.rotation = rotation;
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
            active.Clear();
        }

        List<Item> filteredItems;
        ItemDistanceComparer itemDistanceComparer;
        public Item GetNearestItemOfTypeTo(Vector3 worldPoint, string itemName, Func<Item, bool> condition)
        {
            itemDistanceComparer.targetPosition = worldPoint;
            filteredItems = active;
            filteredItems.RemoveAll( item => ((item.itemName == itemName) && condition(item)));
            filteredItems.Sort(itemDistanceComparer);
            if (filteredItems.Count > 0)
                return filteredItems[0];
            else    
                return null;
        }

        public Item GetNearestItemOfTypeTo(Vector3 worldPoint, string itemName)
        {
            itemDistanceComparer.targetPosition = worldPoint;
            filteredItems = active;
            filteredItems.RemoveAll(item => item.itemName == itemName);
            filteredItems.Sort(itemDistanceComparer);
            if (filteredItems.Count > 0)
                return filteredItems[0];
            else    
                return null;
        }

        public Item GetNearestItemTo(Vector3 worldPoint)
        {
            // Debug.Log(items.Count);
            itemDistanceComparer.targetPosition = worldPoint;
            active.Sort(itemDistanceComparer);
            if (active.Count > 0)
                return active[0];
            else    
                return null;
        }

        class ItemDistanceComparer : IComparer<Item>
        {
            public Vector3 targetPosition;

            public int Compare(Item a, Item b)
            {
                return Vector3.Distance(
                    a.transform.position, targetPosition)
                    .CompareTo(Vector3.Distance(
                    b.transform.position, targetPosition));
            }
        }

    }
}
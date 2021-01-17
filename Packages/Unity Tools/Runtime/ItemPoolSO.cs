using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Item Pool SO", menuName = "Scriptables/Item Pool SO")]
    public class ItemPoolSO : ScriptableObject
    {
        public ItemStorageSO itemStorage;
        public List<Item> pool;
        public List<Item> active;
        [HideInInspector]
        public ItemPoolMB itemPoolMB;

        public Item SpawnFromPool(string itemName)
        {
            Item item = null;
            for(int i = 0; i < pool.Count; i++)
            {
                if (pool[i].itemName == itemName)
                {
                    item = pool[i];
                    item.gameObject.SetActive(true);
                    item.OnUnPooled();
                    pool.Remove(item);
                }
                else
                {
                    Debug.LogWarning("no item of type " + itemName + " found in item pool");
                }
            }
            return item;
        }
        
        public void ReturnToPool(Item item)
        {
            item.OnPooled();
            item.gameObject.transform.position = Vector3.zero;
            item.gameObject.transform.rotation = Quaternion.identity;
            item.gameObject.transform.parent = itemPoolMB.poolParent;
            item.gameObject.SetActive(false);
            pool.Add(item);
        }
        
        public void ReturnAllToPool()
        {
            for (int i = pool.Count-1; i >= 0; i--)
            {
                ReturnToPool(pool[i]);
            }
        }

        public void ClearPool()
        {
            pool.Clear();
        }
    }
}
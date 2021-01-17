using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Edwon.Tools;

namespace Edwon.Tools
{
    public class ItemPoolMB : MonoBehaviour
    {
        public ItemPoolSO itemPoolSO;
        [HideInInspector]
        public Transform poolParent;
        
        [Header("Debug")]
        public bool unfoldInHierarchy = false;

        void Awake()
        {
            itemPoolSO.itemPoolMB = this;
            poolParent = this.transform;
            InitializePool();
            if (unfoldInHierarchy)
                Utils.UnfoldInEditorHierarchy(poolParent);
        }

        public void InitializePool()
        {
            itemPoolSO.pool = new List<Item>();
            itemPoolSO.active = new List<Item>();
            foreach(ItemStorageSO.ItemSlot slot in itemPoolSO.itemStorage.itemSlots)
            {
                for (int i = 0; i < slot.numberInPool; i++)
                {
                    GameObject spawned = GameObject.Instantiate(slot.itemPrefab.gameObject, Vector3.zero, Quaternion.identity);
                    spawned.transform.parent = poolParent;
                    spawned.name = slot.itemPrefab.itemName;
                    Item item = spawned.GetComponent<Item>();
                    itemPoolSO.pool.Add(item);
                    spawned.SetActive(false);
                }
            }
        }
        
        void OnDisable()
        {
            itemPoolSO.ClearPool();
        }

        [InspectorButton("ReturnAllToPool")]
        public bool returnAllToPool;
        public void ReturnAllToPool()
        {
            itemPoolSO.ReturnAllToPool();
        }
    }
}
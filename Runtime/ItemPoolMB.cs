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
        
        void Awake()
        {
            itemPoolSO.itemPoolMB = this;
            poolParent = this.transform;
            itemPoolSO.InitializePool();
            if (unfoldInHierarchy){ Utils.UnfoldInEditorHierarchy(poolParent); }
        }

        void Update()
        {
            if (debugDraw)
                foreach(Item item in itemPoolSO.active)
                    item.debugDraw = true;
        }
        
        void OnDisable()
        {
            itemPoolSO.ClearPool();
        }

        [Header("Test")]
        [InspectorButton("ReturnAllToPool")]
        public bool returnAllToPool;
        public void ReturnAllToPool()
        {
            itemPoolSO.ReturnAllToPool();
        }
        
        [InspectorButton("SpawnTestItem")]
        public bool spawnTestItem;
        public void SpawnTestItem()
        {
            testSpawner.Spawn(testItemName);
        }
        public string testItemName;
        public ItemPoolSpawner testSpawner;


        [Header("Debug")]
        public bool unfoldInHierarchy = false;
        public bool debugDraw = false;
    }
}
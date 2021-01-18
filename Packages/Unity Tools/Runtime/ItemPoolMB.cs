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
        public bool debugDraw = false;
        
        [Header("Debug")]
        public bool unfoldInHierarchy = false;

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

        [InspectorButton("ReturnAllToPool")]
        public bool returnAllToPool;
        public void ReturnAllToPool()
        {
            itemPoolSO.ReturnAllToPool();
        }
    }
}
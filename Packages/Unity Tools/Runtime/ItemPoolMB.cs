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
            itemPoolSO.InitializePool();
            if (unfoldInHierarchy){ Utils.UnfoldInEditorHierarchy(poolParent); }
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
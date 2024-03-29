﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Edwon.Tools 
{
    // Holders can only hold one IHoldable at a time
    public class Holder : MonoBehaviour
    {
        public IHoldable held;
        public bool smoothMovement;
        [ReadOnly]
        [SerializeField]
        string heldNameDebug;
        public Spawner spawner;
        public GameObject spawnAndHoldOnAwakePrefab;

        void Update()
        {
            SetHeldNameDebugStrings();
        }

        void SetHeldNameDebugStrings()
        {
            if (held != null)
                heldNameDebug = held.GameObject.name;
            else
                heldNameDebug = "";
        }

        public void Hold(GameObject toHoldGO)
        {
            IHoldable toHold = toHoldGO.GetComponent<IHoldable>();
            Hold(toHold);
        }

        public void Hold(IHoldable toHold)
        {
            toHold.holder = this;
            held = toHold;
            toHold.SmoothMovement = smoothMovement;
            toHold.OnHold(this);
        }
        
        public void ReleaseAndHold(GameObject toHoldGO)
        {
            Release();
            Hold(toHoldGO);
        }

        public void ReleaseAndHold(IHoldable toHold)
        {
            Release();
            Hold(toHold);
        }

        [InspectorButton("Release")]
        public bool release;
        public void Release()
        {
            if (held == null)
                return;
            
            held.OnRelease();
            held.holder = null;
            held.holderLast = this;
            held = null;
        }

        [InspectorButton("DestroyHeld")]
        public bool destroyHeld;
        public void DestroyHeld()
        {
            if (held != null)
                held.Release(true);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    public class HoldableRigidbody : MonoBehaviour, IHoldable
    {
        #pragma warning disable 0414

        public bool debugLog = false;
        [ReadOnly]
        [SerializeField]
        bool isHeld;
        public string gameObjectName {get;set;}
        public Holder holder {get; set;}
        public Rigidbody rigidbodyToHold;
        Collider[] colliders;
        public bool setChildrenIsKinematicWhileHeld = false;
        public bool disableCollidersWhileHeld = false;
        
        void Awake()
        {
            colliders = GetComponentsInChildren<Collider>();
            
            if (rigidbodyToHold == null)
                rigidbodyToHold = GetComponent<Rigidbody>();

            SetIsHeld();
        }

        void SetIsHeld()
        {
            if (holder == null)
                isHeld = false;
            else
                isHeld = true;
        }

        void FixedUpdate()
        {
            if (holder != null)
                rigidbodyToHold.MovePosition(holder.transform.position);

            SetIsHeld();
        }

        public void Release(bool andDestroy)
        {
            if (holder != null)
                holder.Release();
            if (andDestroy)
                Destroy(gameObject);
        }

        public void OnHold(Holder _holder)
        {
            gameObjectName = gameObject.name;

            if (debugLog)
                Debug.Log(gameObject.name + "OnHold " + gameObject.name + " To:" + _holder.name);
            
            if (disableCollidersWhileHeld)
                ToggleColliders(false);

            SetKinematic(true);
        }

        public void OnRelease()
        {
            if (debugLog)
                Debug.Log(gameObject.name + " OnRelease");

            if (disableCollidersWhileHeld)
                ToggleColliders(true);

            SetKinematic(false);
        }

        public void SetKinematic(bool isKinematic)
        {
            if (setChildrenIsKinematicWhileHeld)
            {
                Rigidbody[] rbs = rigidbodyToHold.GetComponentsInChildren<Rigidbody>();
                foreach(Rigidbody rb in rbs)
                {
                    rb.isKinematic = isKinematic;
                }
            }
            else
            {
                rigidbodyToHold.isKinematic = isKinematic;
            }
        }

        void ToggleColliders(bool toggle)
        {
            foreach(Collider c in colliders)
                c.enabled = toggle;
        }
    }
}
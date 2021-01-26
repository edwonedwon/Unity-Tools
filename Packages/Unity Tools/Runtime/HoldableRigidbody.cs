using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    public class HoldableRigidbody : MonoBehaviour, IHoldable
    {
        #pragma warning disable 0414

        public bool debugLog = false;
        public GameObject GameObject {get;set;}
        public Holder holder {get; set;}
        [HideInInspector]
        public Holder holderLast {get; set;}
        [SerializeField]
        [ReadOnly]
        bool isHeld;
        public bool IsHeld {get{ return IsHeld;}}
        public Rigidbody rigidbodyToHold;
        Collider[] colliders;
        public bool childrenKinematicWhileHeld = false;
        public bool collidersDisabledWhileHeld = false;
        public LayerMask collidersDontToggle;
        public IDestroyable destroyable;
        
        void Awake()
        {
            GameObject = gameObject;
            destroyable = GetComponent<IDestroyable>();
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
            {
                destroyable.DestroySelf();
            }
        }

        public void OnHold(Holder _holder)
        {
            GameObject = gameObject;

            if (debugLog)
                Debug.Log(gameObject.name + "OnHold " + gameObject.name + " To:" + _holder.name);
            
            if (collidersDisabledWhileHeld)
                Utils.ToggleColliders(colliders, false, collidersDontToggle);

            SetKinematic(true);
        }

        public void OnRelease()
        {
            if (debugLog)
                Debug.Log(gameObject.name + " OnRelease");

            if (collidersDisabledWhileHeld)
                Utils.ToggleColliders(colliders, true, collidersDontToggle);

            SetKinematic(false);
        }

        public void SetKinematic(bool isKinematic)
        {
            if (childrenKinematicWhileHeld)
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
    }
}
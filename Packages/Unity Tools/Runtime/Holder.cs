using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    public interface IHoldable
    {
        string gameObjectName {get; set;}
        Holder holder {get; set;}
        Holder holderLast {get; set;}
        void Release(bool andDestroy = false);
        void OnHold(Holder holder);
        void OnRelease();
    }

    // Holders can only hold one IHoldable at a time
    public class Holder : MonoBehaviour
    {
        public IHoldable held;
        IHoldable heldLast;
        [ReadOnly]
        [SerializeField]
        string heldNameDebug;
        [ReadOnly]
        [SerializeField]
        string heldLastNameDebug;
        public Spawner spawner;
        public GameObject spawnAndHoldOnAwakePrefab;

        void Update()
        {
            SetHeldNameDebugStrings();
        }

        void SetHeldNameDebugStrings()
        {
            if (held != null)
                heldNameDebug = held.gameObjectName;
            else
                heldNameDebug = "";

            if (heldLast != null)
                heldLastNameDebug = heldLast.gameObjectName;
            else
                heldLastNameDebug = "";
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
            heldLast = held;
            held = null;
        }

        [InspectorButton("DestroyHeld")]
        public bool destroyHeld;
        public void DestroyHeld()
        {
            Debug.Log("Destroy Held is called (maybe not destroying)");
            if (held != null)
            {
                heldLast = held;
                held.Release(true);
            }
        }
    }
}
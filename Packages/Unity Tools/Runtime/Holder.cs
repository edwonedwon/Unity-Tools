using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.UnityTools 
{
    public interface IHoldable
    {
        string gameObjectName {get; set;}
        Holder holder {get; set;}
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
        public bool spawnAndHoldOnAwake;
        public GameObject spawnAndHoldOnAwakePrefab;
        public PrefabStorageStorage allPrefabs;

        void Awake()
        {
            if (spawnAndHoldOnAwake)
                SpawnAndHold(spawnAndHoldOnAwakePrefab);
        }

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

        public void SpawnAndHold(GameObject holdableToSpawn)
        {
            if (held != null)
            {
                held.Release(true); // release and destroy
            }
            GameObject spawned = spawner.SpawnGiven(holdableToSpawn);
            IHoldable spawnedHoldable = spawned.GetComponent<IHoldable>();
            Hold(spawnedHoldable);
        }

        [InspectorButton("SpawnAndHoldLastHeld")]
        public bool spawnAndHoldLastHeld;
        public void SpawnAndHoldLastHeld()
        {
            if (heldLast == null)
                return; 
                
            string lastHeldPrefabName = Utils.RemoveParenthesisAndInside(heldLast.gameObjectName);
            GameObject lastHeldPrefab = allPrefabs.GetPrefab(lastHeldPrefabName);
            SpawnAndHold(lastHeldPrefab);
        }

        public void Hold(GameObject toHoldGameObject)
        {
            IHoldable toHold = toHoldGameObject.GetComponent<IHoldable>();
            Hold(toHold);
        }

        public void Hold(IHoldable toHold)
        {
            toHold.holder = this;
            held = toHold;
            toHold.OnHold(this);
        }

        public void Release()
        {
            if (held == null)
                return;
            
            held.OnRelease();
            held.holder = null;
            heldLast = held;
            held = null;
        }

        [InspectorButton("DestroyHeld")]
        public bool destroyHeld;
        public void DestroyHeld()
        {
            if (held != null)
            {
                heldLast = held;
                held.Release(true);
            }
        }
    }
}
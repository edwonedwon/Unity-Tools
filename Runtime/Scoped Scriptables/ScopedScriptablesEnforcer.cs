using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Edwon.Tools
{
    [System.Serializable]
    public class ScopedScriptableInstance
    {
        public ScopedScriptable instance;
        public ScopedScriptable asset;

        public ScopedScriptableInstance(ScopedScriptable instance, ScopedScriptable asset)
        {
            this.instance = instance;
            this.asset = asset;
        }
    }

    [DefaultExecutionOrder(-3)]
    public class ScopedScriptablesEnforcer : MonoBehaviour
    {
        // List<IScopedScriptableUser> uniqueScriptableUsers;
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptableInstance> instances = new List<ScopedScriptableInstance>();
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptable> assets = new List<ScopedScriptable>();
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptable> assetsUnfiltered = new List<ScopedScriptable>();

        public delegate void SetScopedScriptableInstance(GameObject owner, List<ScopedScriptableInstance> instances);
        public static event SetScopedScriptableInstance onSetScopedScriptableInstance;

        void Start()
        {
            // filter assets
            assets = assetsUnfiltered.Distinct<ScopedScriptable>().ToList();

            // make instances
            foreach(ScopedScriptable asset in assets)
            {
                ScopedScriptable instance = Instantiate(asset);
                instances.Add(new ScopedScriptableInstance(instance, asset));
            }

            // set asset reference to instances on all IUniqueScriptableUsers
            if (onSetScopedScriptableInstance != null)
                onSetScopedScriptableInstance(gameObject, instances);
        }

        void OnRegisterScopedScriptable(GameObject owner, ScopedScriptable asset)
        {
            if (owner == this.gameObject)
            {
                assetsUnfiltered.Add(asset);
            }
        }

        void OnEnable()
        {
            ScopedScriptable.registerScopedScriptable += OnRegisterScopedScriptable;
        }

        void OnDisable()
        {
            ScopedScriptable.registerScopedScriptable -= OnRegisterScopedScriptable;
        }
    }
}

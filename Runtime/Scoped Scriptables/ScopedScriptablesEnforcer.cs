using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Edwon.Tools
{
    public interface IScopedScriptableUser
    {
        public List<ScopedScriptable> GetScopedScriptables();
        public void SetScopedScriptables(List<ScopedScriptableInstance> instances);
    }

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

    public class ScopedScriptablesEnforcer : MonoBehaviour
    {
        List<IScopedScriptableUser> uniqueScriptableUsers;
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptableInstance> instances = new List<ScopedScriptableInstance>();
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptable> assets = new List<ScopedScriptable>();
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptable> assetsUnfiltered = new List<ScopedScriptable>();

        void Awake()
        {
            // get asset scriptables
            uniqueScriptableUsers = gameObject.GetComponents<IScopedScriptableUser>().ToList();
            foreach(IScopedScriptableUser user in uniqueScriptableUsers)
                foreach (ScopedScriptable original in user.GetScopedScriptables())
                    assetsUnfiltered.Add(original);

            // filter assets
            assets = assetsUnfiltered.Distinct<ScopedScriptable>().ToList();

            // make instances
            foreach(ScopedScriptable asset in assets)
            {
                ScopedScriptable instance = Instantiate(asset);
                instances.Add(new ScopedScriptableInstance(instance, asset));
            }

            // set asset reference to instances on all IUniqueScriptableUsers
            foreach(IScopedScriptableUser user in uniqueScriptableUsers)
            {
                user.SetScopedScriptables(instances);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Edwon.Tools
{
    [System.Serializable]
    public class UniqueScriptableInstance
    {
        public UniqueScriptable instance;
        public UniqueScriptable asset;

        public UniqueScriptableInstance(UniqueScriptable instance, UniqueScriptable asset)
        {
            this.instance = instance;
            this.asset = asset;
        }
    }

    public class UniqueScriptableManager : MonoBehaviour
    {
        List<IUniqueScriptableUser> uniqueScriptableUsers;
        [SerializeField]
        [ReadOnly]
        List<UniqueScriptableInstance> instances = new List<UniqueScriptableInstance>();
        [SerializeField]
        [ReadOnly]
        List<UniqueScriptable> assets = new List<UniqueScriptable>();
        [SerializeField]
        [ReadOnly]
        List<UniqueScriptable> assetsUnfiltered = new List<UniqueScriptable>();

        void Awake()
        {
            // get asset scriptables
            uniqueScriptableUsers = gameObject.GetComponents<IUniqueScriptableUser>().ToList();
            foreach(IUniqueScriptableUser user in uniqueScriptableUsers)
                foreach (UniqueScriptable original in user.GetUniqueScriptables())
                    assetsUnfiltered.Add(original);

            // filter assets
            assets = assetsUnfiltered.Distinct<UniqueScriptable>().ToList();

            // make instances
            foreach(UniqueScriptable asset in assets)
            {
                UniqueScriptable instance = Instantiate(asset);
                instance.isInstance = true;
                instances.Add(new UniqueScriptableInstance(instance, asset));
            }

            // set asset reference to instances on all IUniqueScriptableUsers
            foreach(IUniqueScriptableUser user in uniqueScriptableUsers)
            {
                user.SetUniqueScriptables(instances);
            }
        }
    }
}

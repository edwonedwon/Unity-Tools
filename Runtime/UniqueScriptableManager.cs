using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Edwon.Tools
{
    public class UniqueScriptableManager : MonoBehaviour
    {
        List<IUniqueScriptableUser> uniqueScriptableUsers;
        [SerializeField]
        [ReadOnly]
        List<UniqueScriptable> assets;
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

            // set scriptables to instances
            foreach(UniqueScriptable asset in assets)
            {
                UniqueScriptable instance = Instantiate(asset);
                instance.isInstance = true;;
                foreach(IUniqueScriptableUser user in uniqueScriptableUsers)
                {
                    user.SetUniqueScriptables(instance, asset, assets);
                }
            }
        }
    }
}

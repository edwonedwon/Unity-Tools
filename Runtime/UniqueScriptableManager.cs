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
        List<UniqueScriptable> duplicateOriginals;
        [SerializeField]
        [ReadOnly]
        List<UniqueScriptable> uniqueScriptableOriginals = new List<UniqueScriptable>();

        void Awake()
        {
            // get all unique scriptable originals
            uniqueScriptableUsers = gameObject.GetComponents<IUniqueScriptableUser>().ToList();
            foreach(IUniqueScriptableUser user in uniqueScriptableUsers)
            {
                foreach (UniqueScriptable original in user.GetUniqueScriptableOriginals())
                {
                    uniqueScriptableOriginals.Add(original);
                }
            }

            // find duplicate references to same scriptable
            duplicateOriginals = uniqueScriptableOriginals.Distinct<UniqueScriptable>().ToList();

            // for each duplicate, replace with a new instance
            foreach(UniqueScriptable duplicate in duplicateOriginals)
            {
                UniqueScriptable instance = Instantiate(duplicate);
                foreach(IUniqueScriptableUser user in uniqueScriptableUsers)
                {
                    user.ReplaceUniqueScriptables(duplicate, instance);
                }
            }
        }
    }
}

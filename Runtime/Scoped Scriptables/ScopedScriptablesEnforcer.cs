using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Edwon.Tools
{
    public interface IScopedScriptableUser
    {
        public List<ScopedScriptable> GetScopedScriptables();
        public void SetScopedScriptables(ScopedScriptablesEnforcer enforcer);
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
        public List<GameObject> userGameObjects = new List<GameObject>();
        List<IScopedScriptableUser> userComponents = new List<IScopedScriptableUser>();
        List<IScopedScriptableUser> userBlacklist = new List<IScopedScriptableUser>();
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
            Init();
        }

        void Init()
        {
            userGameObjects.Clear();
            userComponents.Clear();
            assets.Clear();
            assetsUnfiltered.Clear();
            DestroyInstances();
            instances.Clear();

            // get asset scriptables from users
            if (userGameObjects.Count == 0)
                userGameObjects.Add(gameObject);
            foreach(var user in userGameObjects)
            {
                var childUsers = user.GetComponentsInChildren<IScopedScriptableUser>(true).ToList();
                for (int i = childUsers.Count-1; i>=0; i--)
                {
                    if (userBlacklist.Contains(childUsers[i]))
                        childUsers.RemoveAt(i);
                }
                userComponents.AddRange(childUsers);
            }
            foreach(IScopedScriptableUser user in userComponents)
                foreach (ScopedScriptable original in user.GetScopedScriptables())
                    assetsUnfiltered.Add(original);

            // filter assets
            assets = assetsUnfiltered.Distinct<ScopedScriptable>().ToList();

            // make instances
            foreach(ScopedScriptable asset in assets)
            {
                if (asset == null)
                    Debug.Log("ScopedScriptable asset is null, probably a ScopedScriptable variable isn't set in the inspector somewhere");
                ScopedScriptable instance = Instantiate(asset);
                instances.Add(new ScopedScriptableInstance(instance, asset));
            }

            // set asset reference to instances on all IUniqueScriptableUsers
            foreach(IScopedScriptableUser user in userComponents)
            {
                user.SetScopedScriptables(this);
            }
        }

        void DestroyInstances()
        {
            for (int i = instances.Count-1; i>=0; i--)
            {
                Destroy(instances[i].instance);
            }
        }

        public T GetInstance<T>(T asset) where T : ScopedScriptable 
        {

            // if asset is actually an instance, return it
            if (instances.Find(x => x.instance == asset) != null)
                return asset;

            T returnable = instances.Find(x => x.asset == asset).instance as T;
            if (returnable == null)
            {
                Debug.Log("no instance of type " + typeof(T).Name + " found");
                return null;
            }
            return returnable;
        }

        public void RegisterScopedScriptableUser(GameObject go)
        {
            userGameObjects.Add(go);
            Init();
        }

        public void UnregisterScopedScriptableUser(IScopedScriptableUser user)
        {
            userBlacklist.Add(user);
            Init();
            userBlacklist.Clear();
        }

        void OnDestroy()
        {
            for (int i = instances.Count - 1; i >= 0; i--)
            {
                Destroy(instances[i].instance);
            }
        }
    }
}

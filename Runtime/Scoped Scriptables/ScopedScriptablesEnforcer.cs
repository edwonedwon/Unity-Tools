using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

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
        [ShowInInspector]
        List<IScopedScriptableUser> users = new List<IScopedScriptableUser>();
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptableInstance> instances = new List<ScopedScriptableInstance>();
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptable> assets = new List<ScopedScriptable>();

        void Awake()
        {
            // clear lists
            if (userGameObjects.Count == 0)
                userGameObjects.Add(gameObject);
            users.Clear();
            assets.Clear();
            DestroyInstances();

            // get child users of type IScopedScriptableUser
            users = GetChildUsers();
            // collect all the asset references from users
            assets = GetScopedScriptablesFromAllUsers();
            // filter out duplicates
            assets = assets.Distinct<ScopedScriptable>().ToList();
            // make instances
            instances = MakeInstances(assets, true);
            // filter out instance duplicates
            instances = MakeInstancesDistinct(instances);
            // set asset reference to instance reference on all users
            SetScopedScriptablesInAllUsers();
        }

        List<ScopedScriptable> GetScopedScriptablesFromAllUsers()
        {
            List<ScopedScriptable> returnable = new List<ScopedScriptable>();
            foreach(IScopedScriptableUser user in users)
                foreach (ScopedScriptable original in user.GetScopedScriptables())
                    returnable.Add(original);
            return returnable;
        }

        void SetScopedScriptablesInAllUsers()
        {
            foreach(IScopedScriptableUser user in users)
                user.SetScopedScriptables(this);
        }

        List<IScopedScriptableUser> GetChildUsers()
        {
            List<IScopedScriptableUser> returnable = new List<IScopedScriptableUser>();
            foreach(var user in userGameObjects)
            {
                var childUsers = user.GetComponentsInChildren<IScopedScriptableUser>(true).ToList();
                returnable.AddRange(childUsers);
            }
            return returnable;
        }

        List<ScopedScriptableInstance> MakeInstances(List<ScopedScriptable> assets, bool checkForDuplicateInstances = true)
        {
            List<ScopedScriptableInstance> instances = new List<ScopedScriptableInstance>();
            foreach(var asset in assets)
            {
                if (checkForDuplicateInstances)
                {
                    // only make an instance if it doesn't exist already
                    if (instances.Find(x => x.asset == asset) == null)
                        instances.Add(MakeInstance(asset));
                }
                else
                {
                    instances.Add(MakeInstance(asset));
                }
            }
            return instances;
        }

        List<ScopedScriptableInstance> MakeInstancesDistinct(List<ScopedScriptableInstance> instances)
        {
            return instances.DistinctBy(x => x.instance).ToList();
        }

        ScopedScriptableInstance MakeInstance(ScopedScriptable asset)
        {
            return new ScopedScriptableInstance(Instantiate(asset), asset);
        }

        void DestroyInstances()
        {
            for (int i = instances.Count-1; i>=0; i--)
            {
                Destroy(instances[i].instance);
            }
            instances.Clear();
        }

        public T GetInstance<T>(T asset) where T : ScopedScriptable 
        {

            // if asset is actually an instance, return it
            if (instances.Find(x => x.instance == asset) != null)
                return asset;

            T instance = instances.Find(x => x.asset == asset).instance as T;
            if (instance == null)
            {
                Debug.Log("no instance of type " + typeof(T).Name + " found");
                return null;
            }
            return instance;
        }

        public void RegisterScopedScriptableUser(IScopedScriptableUser user)
        {
            // add this user
            users.Add(user);
            // get all scoped scriptables unfiltered from this user
            List<ScopedScriptable> assets = user.GetScopedScriptables();
            // filter out duplicates
            assets = assets.Distinct<ScopedScriptable>().ToList();
            // make instances
            instances = MakeInstances(assets, true);
            // filter out instance duplicates
            instances = MakeInstancesDistinct(instances);
            // set scoped scriptables on the user to instances
            user.SetScopedScriptables(this);
        }

        public void UnregisterScopedScriptableUser(IScopedScriptableUser user)
        {
            users.Remove(user);
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

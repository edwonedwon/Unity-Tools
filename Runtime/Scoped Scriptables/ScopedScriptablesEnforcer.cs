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
        [SerializeField]
        [ReadOnly]
        List<ScopedScriptable> assetsUnfiltered = new List<ScopedScriptable>();

        void Awake()
        {
            Init();
        }

        void Init()
        {
            if (userGameObjects.Count == 0)
                userGameObjects.Add(gameObject);
            users.Clear();
            assets.Clear();
            assetsUnfiltered.Clear();
            DestroyInstances();

            // get child users of type IScopedScriptableUser
            users = GetChildUsers();

            // collect all the asset references from users
            assetsUnfiltered = GetScopedScriptablesFromAllUsers();

            // filter out duplicates
            assets = assetsUnfiltered.Distinct<ScopedScriptable>().ToList();

            // make instances
            instances = MakeInstances();

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

        List<ScopedScriptableInstance> MakeInstances()
        {
            List<ScopedScriptableInstance> returnable = new List<ScopedScriptableInstance>();
            foreach(ScopedScriptable asset in assets)
            {
                if (asset == null)
                    Debug.Log("ScopedScriptable asset is null, probably a ScopedScriptable variable isn't set in the inspector somewhere");
                ScopedScriptable instance = Instantiate(asset);
                returnable.Add(new ScopedScriptableInstance(instance, asset));
            }
            return returnable;
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

            T returnable = instances.Find(x => x.asset == asset).instance as T;
            if (returnable == null)
            {
                Debug.Log("no instance of type " + typeof(T).Name + " found");
                return null;
            }
            return returnable;
        }

        public void RegisterScopedScriptableUser(IScopedScriptableUser user)
        {
            users.Add(user);
            List<ScopedScriptable> newScriptables = user.GetScopedScriptables();
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

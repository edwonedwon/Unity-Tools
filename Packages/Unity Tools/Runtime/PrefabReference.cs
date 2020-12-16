using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Edwon.Tools
{
    [ExecuteInEditMode]
    public class PrefabReference : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private GameObject prefab;
        public GameObject Prefab 
        {
            get
            {
                if (prefab == null)
                    Debug.Log("prefab reference is null, make sure to press GetPrefabReference in editor while not playing");

                return prefab;                
            }
            set
            {
                prefab = value;
            }
        }

        #if UNITY_EDITOR
        [InspectorButton("GetPrefabReference")]
        public bool getPrefabReference;
        private void GetPrefabReference()
        {
            // See https://docs.unity3d.com/ScriptReference/PrefabUtility.GetCorrespondingObjectFromOriginalSource.html
            prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
        }
        #endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Edwon.Tools
{
    public class EditorOnly : MonoBehaviour
    {
        void Awake()
        {
            #if !UNITY_EDITOR
            gameObject.SetActive(false);
            #endif
        }
    }
}
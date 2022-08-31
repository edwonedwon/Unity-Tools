using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Edwon.Tools
{
    public class EditorOnly : MonoBehaviour
    {
        public bool dontDisableInDesktopBuild;

        void Awake()
        {
            #if !UNITY_EDITOR
            if (dontDisableInDesktopBuild)
                if (SystemInfo.deviceType == DeviceType.Desktop)
                    return;

            gameObject.SetActive(false);
            #endif
        }
    }
}
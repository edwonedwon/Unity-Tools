using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.UnityTools 
{
    [RequireComponent(typeof(RectTransform))]
    public class DeviceSpecificRectTransform : MonoBehaviour
    {
        RectTransform rect;
        
        [Header("iPhoneX")]
        public int iPhoneXHeight;

        [Header("All Other")]
        public int otherHeight;

        void Awake()
        {
            rect = GetComponent<RectTransform>();
            
            if (IsIPhoneXGeneration()) // if iPhoneX
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, iPhoneXHeight);
            }
            else // if other
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, otherHeight);
            }
        }

        bool IsIPhoneXGeneration()
        {
            if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneX 
            || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneXR
            || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneXS
            || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneXSMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
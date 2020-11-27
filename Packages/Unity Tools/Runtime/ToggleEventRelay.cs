using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.UnityTools 
{
    [System.Serializable]
    public class OnToggleUnityEvent : UnityEvent<bool> {}

    public class ToggleEventRelay : MonoBehaviour
    {
        [Header("if not null, will only listen to toggles with this name")]
        public string toggleName = "";
        public OnToggleUnityEvent onToggle;

        public bool checkToggleValueOnAwake;
        public delegate bool? GetToggleValueEvent(string name);
        public static GetToggleValueEvent getToggleValueEvent;

        void Awake()
        {
            if (checkToggleValueOnAwake)
            {
                if (getToggleValueEvent != null)
                {
                    bool? toggleValue = getToggleValueEvent(toggleName);
                    if (toggleValue != null)
                    {
                        onToggle.Invoke(toggleValue.Value);
                    }
                }
            }
        }

        void OnToggleEvent(bool toValue, string name)
        {
            if (toggleName != "")
                if (name != toggleName)
                    return;

            if (onToggle != null)
                onToggle.Invoke(toValue);
        }

        void OnEnable()
        {
            ToggleEventBroadcaster.onToggleEvent += OnToggleEvent;
        }

        void OnDisable()
        {
            ToggleEventBroadcaster.onToggleEvent -= OnToggleEvent;
        }
    }
}
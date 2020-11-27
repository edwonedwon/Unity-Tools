using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Edwon.UnityTools 
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleEventBroadcaster : MonoBehaviour
    {
        Toggle toggle;

        public delegate void OnToggleEvent(bool toValue, string name);
        public static OnToggleEvent onToggleEvent;

        void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        public void OnToggle(bool toValue)
        {
            if (onToggleEvent != null)
                onToggleEvent(toValue, gameObject.name);
        }

        bool? OnGetToggleValueEvent(string name)
        {
            if (name == gameObject.name)
            {
                return toggle.isOn;
            }
            return null;
        }

        void OnEnable()
        {
            ToggleEventRelay.getToggleValueEvent += OnGetToggleValueEvent;
        }

        void OnDisable()
        {
            ToggleEventRelay.getToggleValueEvent -= OnGetToggleValueEvent;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Edwon.UnityTools 
{
    [RequireComponent(typeof(Slider))]
    public class SliderEventBroadcaster : MonoBehaviour
    {
        Slider slider;

        public delegate void OnSliderValueChangedEvent(float toValue, string name);
        public static OnSliderValueChangedEvent onSliderValueChangedEvent;

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void OnSliderValueChanged(float toValue)
        {
            if (onSliderValueChangedEvent != null)
                onSliderValueChangedEvent(toValue, gameObject.name);
        }

        float? OnGetSliderValueEvent(string name)
        {
            if (name == gameObject.name)
            {
                return slider.value;
            }
            return null;
        }

        void OnEnable()
        {
            SliderEventRelay.getSliderValueEvent += OnGetSliderValueEvent;
        }

        void OnDisable()
        {
            SliderEventRelay.getSliderValueEvent -= OnGetSliderValueEvent;
        }
    }
}
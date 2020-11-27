using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.UnityTools 
{
    [System.Serializable]
    public class OnSliderValueChangedUnityEvent : UnityEvent<float> { }

    public class SliderEventRelay : MonoBehaviour
    {
        [Header("if not null, will only listen to sliders with this name")]
        public string sliderName = "";
        public OnSliderValueChangedUnityEvent onSliderValueChangedEvent;

        public bool checkSliderValueOnAwake;
        public delegate float? GetSliderValueEvent(string name);
        public static GetSliderValueEvent getSliderValueEvent;

        void Awake()
        {
            if (checkSliderValueOnAwake)
            {
                if (getSliderValueEvent != null)
                {
                    float? sliderValue = getSliderValueEvent(sliderName);
                    if (sliderValue != null)
                    {
                        onSliderValueChangedEvent.Invoke(sliderValue.Value);
                    }
                }
            }
        }

        void OnSliderValueChangedEvent(float toValue, string name)
        {
            if (sliderName != "")
                if (name != sliderName)
                    return;

            if (onSliderValueChangedEvent != null)
                onSliderValueChangedEvent.Invoke(toValue);
        }

        void OnEnable()
        {
            SliderEventBroadcaster.onSliderValueChangedEvent += OnSliderValueChangedEvent;
        }

        void OnDisable()
        {
            SliderEventBroadcaster.onSliderValueChangedEvent -= OnSliderValueChangedEvent;
        }
    }
}
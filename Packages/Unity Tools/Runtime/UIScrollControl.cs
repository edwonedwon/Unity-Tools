using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Edwon.UnityTools 
{
    [RequireComponent(typeof(ScrollRect))]
    public class UIScrollControl : MonoBehaviour
    {
        ScrollRect scroll;
        public bool setScrollPositionOnAwake = true;
        public Vector2 scrollPositionOnAwake;

        void Awake()
        {
            scroll = GetComponent<ScrollRect>();
            
            if (setScrollPositionOnAwake)
                scroll.normalizedPosition = scrollPositionOnAwake;
        }

        [InspectorButton("SetScrollPositionToSet")]
        public bool setScrollPositionToSet;
        public void SetScrollPositionToSet()
        {
            scroll.normalizedPosition = setScrollPosition;
        }
        public Vector2 setScrollPosition;
    }
}
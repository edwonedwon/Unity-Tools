using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace Edwon.Tools 
{
    public class PointerEventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IScrollHandler
    {
        [Serializable]
        public class OnPointerUpEvent : UnityEvent<PointerEventData> { }
        public OnPointerUpEvent onPointerUpEvent;
        [Serializable]
        public class OnPointerDownEvent : UnityEvent<PointerEventData> { }
        public OnPointerDownEvent onPointerDownEvent;
        [Serializable]
        public class OnPointerClickEvent : UnityEvent<PointerEventData> { }
        public OnPointerClickEvent onPointerClickEvent;
        [Serializable]
        public class OnDragEvent : UnityEvent<PointerEventData> { }
        public OnDragEvent onDragEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClickEvent.Invoke(eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            onPointerUpEvent.Invoke(eventData);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            onPointerDownEvent.Invoke(eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            onDragEvent.Invoke(eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {

        }

        public virtual void OnScroll(PointerEventData eventData)
        {

        }
    }
}
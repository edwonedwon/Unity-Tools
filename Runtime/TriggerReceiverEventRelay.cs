using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class TriggerReceiverEventRelay : MonoBehaviour, ITriggerReceiver
    {
        public UnityEvent onTriggerEnter;
        public UnityEvent onTriggerStay;
        public UnityEvent onTriggerExit;

        public void OnTriggerEnter(Collider collider)
        {
            onTriggerEnter.Invoke();
        }

        public void OnTriggerStay(Collider collider)
        {
            onTriggerStay.Invoke();
        }

        public void OnTriggerExit(Collider collider)
        {
            onTriggerExit.Invoke();
        }
    }
}
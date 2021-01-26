using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class CollisionReceiverEventRelay : MonoBehaviour, ICollisionReceiver
    {
        public UnityEvent onCollisionEnter;
        public UnityEvent onCollisionStay;
        public UnityEvent onCollisionExit;

        public void OnCollisionEnter(Collision collider)
        {
            onCollisionEnter.Invoke();
        }

        public void OnCollisionStay(Collision collider)
        {
            onCollisionStay.Invoke();
        }

        public void OnCollisionExit(Collision collider)
        {
            onCollisionExit.Invoke();
        }
    }
}
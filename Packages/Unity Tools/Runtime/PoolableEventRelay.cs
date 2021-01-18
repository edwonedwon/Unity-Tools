using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class PoolableEventRelay : MonoBehaviour, IPoolable
    {
        public UnityEvent onPooled;
        public UnityEvent onUnPooled;

        public void OnPooled()
        {
            onPooled.Invoke();    
        }

        public void OnUnPooled()
        {
            onUnPooled.Invoke();
        }
    }
}
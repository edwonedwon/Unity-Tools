using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    public interface ITriggerReceiver
    {
        void OnTriggerEnter(Collider collider);
        void OnTriggerStay(Collider collider);
        void OnTriggerExit(Collider collider);
    }

    public class TriggerSender : MonoBehaviour
    {
        public GameObject TriggerReceiver;
        ITriggerReceiver receiver;

        void Awake()
        {
            receiver = TriggerReceiver.GetComponent<ITriggerReceiver>();
        }

        void OnTriggerEnter(Collider collider)
        {
            receiver.OnTriggerEnter(collider);
        }

        void OnTriggerStay(Collider collider)
        {
            receiver.OnTriggerStay(collider);
        }

        void OnTriggerExit(Collider collider)
        {
            receiver.OnTriggerExit(collider);
        }
    }
}
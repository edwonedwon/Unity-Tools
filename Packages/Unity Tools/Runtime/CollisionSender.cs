using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    public interface ICollisionReceiver
    {
        void OnCollisionEnter(Collision collision);
        void OnCollisionStay(Collision collision);
        void OnCollisionExit(Collision collision);
    }

    public class CollisionSender : MonoBehaviour
    {
        public GameObject collisionReceiver;
        ICollisionReceiver receiver;

        void Awake()
        {
            receiver = collisionReceiver.GetComponent<ICollisionReceiver>();
        }

        void OnCollisionEnter(Collision collision)
        {
            receiver.OnCollisionEnter(collision);
        }

        void OnCollisionStay(Collision collision)
        {
            receiver.OnCollisionStay(collision);
        }

        void OnCollisionExit(Collision collision)
        {
            receiver.OnCollisionExit(collision);
        }
    }
}
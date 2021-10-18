using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Edwon.Tools 
{
    public class DoNotCollideWithSelf : MonoBehaviour
    {
        public Collider[] exceptions;

        void Awake()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            for (int i=0; i<colliders.Length; i++)
            {
                for (int j = 0; j<colliders.Length; j++)
                {
                    if (!exceptions.Contains(colliders[i]) && !exceptions.Contains(colliders[j]))
                    {
                        Physics.IgnoreCollision(colliders[i], colliders[j], true);
                    }
                }
            }
        }
    }
}
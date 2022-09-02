using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class FaceMainCamera : MonoBehaviour
    {
        public bool inverse = false;

        void Update()
        {
            if (inverse)
                transform.forward = transform.position - Camera.main.transform.position;
            else
                transform.forward = Camera.main.transform.position - transform.position;
        }
    }
}
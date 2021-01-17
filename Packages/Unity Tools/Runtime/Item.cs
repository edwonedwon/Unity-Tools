using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Edwon.Tools
{
    public class Item : MonoBehaviour
    {
        public string itemName;
        List<Renderer> renderers;
        public bool useWithPool = true;
        public ItemPoolSO itemPoolSO;

        private void Awake() 
        {
            renderers = transform.GetComponentsInChildren<Renderer>().ToList();
        }

        public void OnPooled()
        {

        }

        public void OnUnPooled()
        {

        }

        public void DestroySelf()
        {
            if (useWithPool)
                itemPoolSO.ReturnToPool(this);
            else
                Destroy(gameObject);
        }

        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;
            if (renderers == null)
                return;
            if (renderers.Count == 0)
                return;

            // First find a center for your bounds.
            Vector3 childCenters = Vector3.zero;

            foreach (Renderer r in renderers)
            {
                if (!(r is ParticleSystemRenderer))
                    childCenters += r.bounds.center;   
            }
            Vector3 centerAverage = transform.TransformPoint(childCenters/renderers.Count); //center is average center of children
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(centerAverage, 0.01f);

            //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
            Bounds bounds = new Bounds(); 

            foreach (Renderer r in renderers)
            {
                if (!(r is ParticleSystemRenderer))
                    bounds.Encapsulate(r.bounds);  
            }

            Gizmos.color = Color.white;
            // Edwon.Tools.Utils.DrawBounds(bounds, Color.white);
            Gizmos.DrawWireCube(centerAverage, bounds.extents);
        }
    }
}
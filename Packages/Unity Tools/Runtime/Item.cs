using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public interface IDestroyable
    {
        void DestroySelf();
    }

    public class Item : MonoBehaviour, IDestroyable
    {
        public string itemName;
        public const string itemTag = "Item";
        List<Renderer> renderers;
        List<IPoolable> poolables;
        public ItemPoolSO itemPoolSO;
        public UnityEvent onDestroySelf;
        public bool debugDraw = false;

        private void Awake() 
        {
            gameObject.tag = itemTag;
            renderers = transform.GetComponentsInChildren<Renderer>().ToList();
            poolables = transform.GetComponentsInChildren<IPoolable>().ToList();
        }

        public void OnPooled()
        {
            foreach(IPoolable poolable in poolables)
                poolable.OnPooled();
        }

        public void OnUnPooled()
        {
            foreach(IPoolable poolable in poolables)
                poolable.OnUnPooled();
        }

        public void DestroySelf()
        {
            onDestroySelf.Invoke();
            
            if (itemPoolSO != null)
                itemPoolSO.ReturnToPool(this);
            else
                Destroy(gameObject);
        }

        void OnDrawGizmos()
        {
            if (!debugDraw)
                return;
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
            // Gizmos.DrawSphere(centerAverage, 0.01f);

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
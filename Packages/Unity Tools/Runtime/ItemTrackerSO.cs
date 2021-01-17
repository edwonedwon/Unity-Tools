using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Tracker", menuName = "Scriptables/Item Tracker")]
public class ItemTrackerSO : ScriptableObject
{
    public List<Item> items;
    private List<Vector3> itemPositions;

    private static ItemDistanceComparer itemDistanceComparer;

    static bool debugLog = false;
    public bool debugDraw = false;

    private void Awake() 
    {
        items = new List<Item>();
        itemPositions = new List<Vector3>();
        itemDistanceComparer = new ItemDistanceComparer();
    }

    public void AddItem(Item item)
    {
        if (!items.Contains(item))
        {            
            if (debugLog)
                Debug.Log("add item ");

            items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            if (debugLog)
                Debug.Log("remove item");

            items.Remove(item);
            items.TrimExcess();
        }
    }

    Item[] filteredItems = new Item[1000];
    public Item GetNearestItemOfTypeTo(Vector3 worldPoint, string itemType)
    {
        itemDistanceComparer.targetPosition = worldPoint;
        items.Sort(itemDistanceComparer);
        if (items.Count > 0)
            return items[0];
        else    
            return null;
    }

    public Item GetNearestItemTo(Vector3 worldPoint)
    {
        // Debug.Log(items.Count);
        itemDistanceComparer.targetPosition = worldPoint;
        items.Sort(itemDistanceComparer);
        if (items.Count > 0)
            return items[0];
        else    
            return null;
    }

    class ItemDistanceComparer : IComparer<Item>
    {
        public Vector3 targetPosition;

        public int Compare(Item a, Item b)
        {
            return Vector3.Distance(
                a.transform.position, targetPosition)
                .CompareTo(Vector3.Distance(
                b.transform.position, targetPosition));
        }
    }

    // ToDo reimplement somehow, doesn't work if it's a scriptable object
    // maybe each item could render it's own gizmo
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (items == null)
            return;

        if (debugDraw)
        {
            foreach(Item item in items)
            {
                int childCount = item.transform.childCount;

                // First find a center for your bounds.
                Vector3 childCenters = Vector3.zero;

                foreach (Transform child in item.transform)
                {
                    Renderer r = child.GetComponent<Renderer>();
                    if (r != null　&& !(r is ParticleSystemRenderer))
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawSphere(r.bounds.center, 0.01f);
                        childCenters += r.bounds.center;   
                    }
                }
                Vector3 centerAverage = item.transform.TransformPoint(childCenters/childCount); //center is average center of children
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(centerAverage, 0.01f);

                //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
                Bounds bounds = new Bounds(); 

                foreach (Transform child in item.transform)
                {
                    Renderer r = child.GetComponent<Renderer>();
                    if (r != null　&& !(r is ParticleSystemRenderer))
                    {
                        bounds.Encapsulate(r.bounds);  
                    }
                }

                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(centerAverage, bounds.size);
            }
        }
    }
}

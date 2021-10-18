using System.Collections.Generic;
using UnityEngine;

public class DistanceComparer : IComparer<Vector3>
{
    public Vector3 target;

    public int Compare(Vector3 a, Vector3 b)
    {
        var targetPosition = target;
        return Vector3.Distance(a, targetPosition).CompareTo(Vector3.Distance(b, targetPosition));
    }
}

// example use
// 
// public Transform[] trees;
// private DistanceComparer distanceComparer;

// private void Awake()
// {
//     distanceComparer = new DistanceComparer(transform);
// }

// private void Update()
// {
//     Array.Sort(trees, distanceComparer);
// }
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this interface is for other components that want to listen to pool events
// on the same game object as the poolable
public interface IPoolable
{
    string PoolableType {get;set;}
    GameObject GameObject {get; set;}
    void OnPooled();
    void OnUnPooled();
}
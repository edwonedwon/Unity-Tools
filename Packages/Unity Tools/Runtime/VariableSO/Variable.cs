using UnityEngine;
using System;

namespace  Edwon.Tools
{
    public class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T initialValue;

        [NonSerialized]
        public T runtimeValue;

        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            Debug.Log("on after deserialize");
            runtimeValue = initialValue;
        }
    }
}
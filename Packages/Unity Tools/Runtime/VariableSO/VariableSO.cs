using UnityEngine;
using System;

namespace  Edwon.Tools
{
    public class VariableSO<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T initialValue;

        [NonSerialized]
        public T runtimeValue;

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }

        public void OnBeforeSerialize() {}
    }
}
using UnityEngine;
using System;

namespace  Edwon.Tools
{
    public class VariableSO<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T initialValue;

        [NonSerialized]
        public T RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = initialValue;
        }

        public void OnBeforeSerialize() {}
    }
}
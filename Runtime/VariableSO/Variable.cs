using UnityEngine;
using System;

namespace  Edwon.Tools
{
    public class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T initialValue;
        virtual public T RuntimeValue {get; set;}

        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            RuntimeValue = initialValue;
        }
    }
}
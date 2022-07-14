using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace  Edwon.Tools
{
    public class Variable<T> : ScopedScriptable, ISerializationCallbackReceiver
    {
        [PropertyOrder(0)]
        [ShowIf("@!UnityEngine.Application.isPlaying")]
        public T initialValue;
        [ShowInInspector]
        [PropertyOrder(1)]
        [ShowIf("@UnityEngine.Application.isPlaying")]
        virtual public T RuntimeValue {get; set;}

        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            RuntimeValue = initialValue;
        }
    }
}
using UnityEngine;
using System;

namespace  Edwon.Tools
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Scriptables/Int Variable")]
    public class IntVariableSO : ScriptableObject, ISerializationCallbackReceiver
    {
        public int initialValue;

        [NonSerialized]
        public int runtimeValue;

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }

        public void OnBeforeSerialize() {}
    }
}
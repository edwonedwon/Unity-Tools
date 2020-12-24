using UnityEngine;
using System;

namespace  Edwon.Tools
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Scriptables/Int Variable")]
    public class IntVariableSO : VariableSO<int>, ISerializationCallbackReceiver 
    {
        public bool alwaysPositive;
        new public int runtimeValue 
        {
            get
            {
                return base.runtimeValue;
            }
            set
            {
                if (alwaysPositive)
                    if (value < 0)
                        return;

                base.runtimeValue = value;
            }
        }
    }
}
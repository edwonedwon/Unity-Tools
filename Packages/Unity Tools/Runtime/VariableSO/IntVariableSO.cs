using UnityEngine;
using System;

namespace  Edwon.Tools
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Scriptables/Int Variable")]
    public class IntVariableSO : VariableSO<int>, ISerializationCallbackReceiver 
    {
        public bool alwaysPositive;
        public int RuntimeValue 
        {
            get
            {
                return base.RuntimeValue;
            }
            set
            {
                if (alwaysPositive)
                    if (value < 0)
                        return;

                base.RuntimeValue = value;
            }
        }
    }
}
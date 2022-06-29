using UnityEngine;
using System;

namespace  Edwon.Tools
{
    [Serializable]
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Scriptables/Int Variable")]
    public class VariableInt : Variable<int>
    {
        public bool alwaysPositive;
        [SerializeField]
        [ReadOnly]
        private int runtimeValue;
        override public int RuntimeValue 
        {
            get
            {
                return runtimeValue;
            }
            set
            {
                if (alwaysPositive)
                    if (value < 0)
                        return;

                runtimeValue = value;
            }
        }
    }
}
using UnityEngine;
using System;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Float Variable", menuName = "Scriptables/Float Variable")]
    public class VariableFloat : Variable<float>
    {
        public bool alwaysPositive;
        [SerializeField]
        [ReadOnly]
        private float runtimeValue;
        override public float RuntimeValue 
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
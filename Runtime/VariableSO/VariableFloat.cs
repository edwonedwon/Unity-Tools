using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Variable Float", menuName = "Variables/Variable Float")]
    public class VariableFloat : Variable<float>
    {
        public bool alwaysPositive;
        [SerializeField]
        [ReadOnly]
        protected float runtimeValue;
        override public float RuntimeValue 
        {
            get
            {
                return runtimeValue;
            }
            set
            {
                if (alwaysPositive)
                {
                    if (value < 0)
                    {
                        runtimeValue = 0;
                        return;
                    }
                }

                runtimeValue = value;
            }
        }
    }
}
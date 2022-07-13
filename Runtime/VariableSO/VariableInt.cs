using UnityEngine;
using System;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Variable Int", menuName = "Variables/Variable Int")]
    public class VariableInt : Variable<int>
    {
        public bool alwaysPositive;
        [SerializeField]
        [ReadOnly]
        protected int runtimeValue;
        override public int RuntimeValue 
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
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Variable Int", menuName = "Variables/Variable Int")]
    public class VariableInt : Variable<int>
    {
        [PropertyOrder(2)]
        public bool alwaysPositive;
        protected int runtimeValue;

        [ShowInInspector]
        [PropertyOrder(1)]
        [ShowIf("@UnityEngine.Application.isPlaying")]
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
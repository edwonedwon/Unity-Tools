using UnityEngine;
using System;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Variable Bool", menuName = "Variables/Variable Bool")]
    public class VariableBool : Variable<bool>
    {
        [SerializeField]
        [ReadOnly]
        protected bool runtimeValue;
        override public bool RuntimeValue
        {
            get
            {
                return runtimeValue;
            }
            set
            {
                runtimeValue = value;
            }
        }
    }
}
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Variable Bool", menuName = "Variables/Variable Bool")]
    public class VariableBool : Variable<bool>
    {
        [SerializeField]
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
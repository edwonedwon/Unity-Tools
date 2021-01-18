using UnityEngine;
using System;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Float Variable", menuName = "Scriptables/Float Variable")]
    public class VariableFloat : Variable<float>
    {
        public bool alwaysPositive;
        new public float runtimeValue 
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
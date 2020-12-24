using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class IntVariableSOSetter : MonoBehaviour
    {
        public IntVariableSO intVariableSO;

        public void SetTo(int value)
        {
            intVariableSO.RuntimeValue = value;
        }

        [InspectorButton("PlusOne")]
        public bool plusOne;
        public void PlusOne()
        {
            intVariableSO.RuntimeValue += 1;
        }

        [InspectorButton("MinusOne")]
        public bool minusOne;
        public void MinusOne()
        {
            intVariableSO.RuntimeValue -= 1;
        }

        [InspectorButton("DebugSetTo")]
        public bool debugSetTo;
        public void DebugSetTo()
        {
            SetTo(debugSetToValue);
        }
        public int debugSetToValue;

    }
}
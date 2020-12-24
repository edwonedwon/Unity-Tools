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
            intVariableSO.runtimeValue = value;
        }

        [InspectorButton("PlusOne")]
        public bool plusOne;
        public void PlusOne()
        {
            intVariableSO.runtimeValue += 1;
        }

        [InspectorButton("MinusOne")]
        public bool minusOne;
        public void MinusOne()
        {
            intVariableSO.runtimeValue -= 1;
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
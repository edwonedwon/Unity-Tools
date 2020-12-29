using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class IntVariableSOSetter : MonoBehaviour
    {
        public IntVariableSO intVariableSO;
        public UnityEventInt onValueChangeSuccess;
        public UnityEventInt onValueChangeSuccessAboveZero;
        public UnityEventInt onValueChangeFail;

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
            int beforeValue = intVariableSO.runtimeValue;
            intVariableSO.runtimeValue -= 1;
            int newValue = intVariableSO.runtimeValue;
            if (newValue != beforeValue)
            {
                onValueChangeSuccess.Invoke(newValue);             
                if (newValue > 0)
                    onValueChangeSuccessAboveZero.Invoke(newValue);
            }
            else
                onValueChangeFail.Invoke(newValue);
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
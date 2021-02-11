using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class VariableIntSetter : MonoBehaviour
    {
        public VariableInt intVariableSO;
        public UnityEventInt onValueChangeSuccess;
        public UnityEventInt onValueChangeSuccessAboveZero;
        public UnityEventInt onValueChangeFail;

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
            int beforeValue = intVariableSO.RuntimeValue;
            intVariableSO.RuntimeValue -= 1;
            int newValue = intVariableSO.RuntimeValue;
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
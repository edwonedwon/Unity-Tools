using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class IntVariableListener : MonoBehaviour
    {
        public IntVariableSO intVariable;
        int intLast;
        public UnityEventInt onIntVariableChanged;

        void Update() 
        {
            if (intVariable.runtimeValue != intLast)
                onIntVariableChanged.Invoke(intVariable.runtimeValue);

            intLast = intVariable.runtimeValue;
        }
    }
}
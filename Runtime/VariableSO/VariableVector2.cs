using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Variable Vector2", menuName = "Variables/Variable Vector2")]
    public class VariableVector2 : Variable<Vector2>
    {
        protected Vector2 runtimeValue;

        [ShowInInspector]
        [PropertyOrder(1)]
        [ShowIf("@UnityEngine.Application.isPlaying")]
        override public Vector2 RuntimeValue {get; set;}
    }
}
using UnityEngine;
using System;

namespace  Edwon.Tools
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Scriptables/Int Variable")]
    public class IntVariableSO : VariableSO<int>, ISerializationCallbackReceiver {}
}
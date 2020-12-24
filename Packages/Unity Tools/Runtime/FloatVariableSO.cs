using UnityEngine;
using System;

namespace Edwon.Tools
{
    [CreateAssetMenu(fileName = "Float Variable", menuName = "Scriptables/Float Variable")]
    public class FloatVariableSO : VariableSO<float>, ISerializationCallbackReceiver {}
}
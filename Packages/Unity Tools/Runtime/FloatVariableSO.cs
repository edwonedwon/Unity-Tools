using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Float Variable", menuName = "Scriptables/Float Variable")]
public class FloatVariableSO : ScriptableObject, ISerializationCallbackReceiver
{
	public float initialValue;

	[NonSerialized]
	public float runtimeValue;

    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }

    public void OnBeforeSerialize() {}
}
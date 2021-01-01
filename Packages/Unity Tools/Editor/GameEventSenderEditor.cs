using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Edwon.Tools
{
    [CustomEditor(typeof(GameEventSender))]
    public class GameEventSenderEditor : Editor
    {
        SerializedProperty gameEventProperty;

        void OnEnable()
        {
            gameEventProperty = serializedObject.FindProperty("gameEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(gameEventProperty);

            if (gameEventProperty.objectReferenceValue != null)
            {
                SerializedObject gameEventObject = new SerializedObject(gameEventProperty.objectReferenceValue);
                SerializedProperty parameterTypeProperty = gameEventObject.FindProperty("parameterType");
                if (parameterTypeProperty != null)
                {
                    // render string showing which paramater type this event is
                    GameEventChannelSO.GameEventParameterType parameterTypeEnum = 
                        (GameEventChannelSO.GameEventParameterType)parameterTypeProperty.enumValueIndex;
                    
                    EditorGUILayout.LabelField("Type:   " + parameterTypeEnum.ToString());
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
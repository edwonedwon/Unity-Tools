using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Edwon.Tools
{
    [CustomEditor(typeof(GameEventListener))]
    public class GameEventListenerEditor : Editor
    {
        SerializedProperty gameEventProperty;
        SerializedProperty response;
        SerializedProperty responseString;
        SerializedProperty responseGameObject;

        void OnEnable()
        {
            gameEventProperty = serializedObject.FindProperty("gameEvent");
            response = serializedObject.FindProperty("response");
            responseString = serializedObject.FindProperty("responseString");
            responseGameObject = serializedObject.FindProperty("responseGameObject");
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
                    
                    EditorGUILayout.Separator();

                    // EditorGUILayout.LabelField("Parameter Type:    " + parameterTypeEnum.ToString());
                    
                    // EditorGUILayout.Separator();

                    // only render the appropriate response action
                    switch(parameterTypeEnum)
                    {
                        case GameEventChannelSO.GameEventParameterType.None:
                        {
                            EditorGUILayout.PropertyField(response);
                        }
                        break;
                        case GameEventChannelSO.GameEventParameterType.String:
                        {
                            EditorGUILayout.PropertyField(responseString);
                        }
                        break;
                        case GameEventChannelSO.GameEventParameterType.GameObject:
                        {
                            EditorGUILayout.PropertyField(responseGameObject);
                        }
                        break;
                    }
                }
            }
                
            serializedObject.ApplyModifiedProperties();
        }
    }
}
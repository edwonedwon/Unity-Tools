#if UNITY_EDITOR

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
        SerializedProperty responseBool;
        SerializedProperty responseBoolTrue;
        SerializedProperty responseBoolFalse;
        SerializedProperty responseInt;
        SerializedProperty responseFloat;
        SerializedProperty responseString;
        SerializedProperty responseGameObject;

        void OnEnable()
        {
            gameEventProperty = serializedObject.FindProperty("gameEvent");
            response = serializedObject.FindProperty("response");
            responseBool = serializedObject.FindProperty("responseBool");
            responseBoolTrue = serializedObject.FindProperty("responseBoolTrue");
            responseBoolFalse = serializedObject.FindProperty("responseBoolFalse");
            responseInt = serializedObject.FindProperty("responseInt");
            responseFloat = serializedObject.FindProperty("responseFloat");
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
                    GameEvent.ParameterType parameterTypeEnum = 
                        (GameEvent.ParameterType)parameterTypeProperty.enumValueIndex;
                    
                    EditorGUILayout.Separator();

                    // EditorGUILayout.LabelField("Parameter Type:    " + parameterTypeEnum.ToString());
                    
                    // EditorGUILayout.Separator();

                    // only render the appropriate response action
                    switch(parameterTypeEnum)
                    {
                        case GameEvent.ParameterType.None:
                        {
                            EditorGUILayout.PropertyField(response);
                        }
                        break;
                        case GameEvent.ParameterType.Bool:
                        {
                            EditorGUILayout.PropertyField(responseBool);
                            EditorGUILayout.PropertyField(responseBoolTrue);
                            EditorGUILayout.PropertyField(responseBoolFalse);
                        }
                        break;
                        case GameEvent.ParameterType.Int:
                        {
                            EditorGUILayout.PropertyField(responseInt);
                        }
                        break;
                        case GameEvent.ParameterType.Float:
                        {
                            EditorGUILayout.PropertyField(responseFloat);
                        }
                        break;
                        case GameEvent.ParameterType.String:
                        {
                            EditorGUILayout.PropertyField(responseString);
                        }
                        break;
                        case GameEvent.ParameterType.GameObject:
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

#endif
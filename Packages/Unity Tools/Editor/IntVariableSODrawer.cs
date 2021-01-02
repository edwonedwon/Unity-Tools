#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;

namespace Edwon.Tools
{
    [CustomPropertyDrawer(typeof(IntVariableSO))]
    public class IntVariableSODrawer : PropertyDrawer
    {
        int lineCount = 2;
        const int lineCountFoldout = 3;
        const float foldoutX = 10f;
        const float foldoutRightX = 100f;
        float lineHeight;
        const float previewPadding = 60f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            lineHeight = EditorGUIUtility.singleLineHeight;
            Rect mainPropertyRect = new Rect(position.x, position.y, position.width - previewPadding, lineHeight);
			EditorGUI.PropertyField(mainPropertyRect, property, label);
            
            IntVariableSO intVariableSO = null;

            if (property.objectReferenceValue != null)
                intVariableSO = EditorUtils.GetTargetObjectOfProperty(property) as IntVariableSO;
            
            if (intVariableSO == null)
            {
                property.isExpanded = false;
                return;
            }
            
            if (intVariableSO != null)
            {
                float previewX = position.x + position.width - previewPadding;
                Rect previewRect = new Rect(previewX, position.y, previewPadding, lineHeight);
                EditorGUI.LabelField(previewRect, intVariableSO.runtimeValue.ToString());
            }
            var foldoutButtonRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
            var foldoutGuiContent = new GUIContent(property.displayName);
            property.isExpanded = EditorGUI.Foldout(foldoutButtonRect, property.isExpanded, foldoutGuiContent, true);

                
            // Foldout
            if (property.isExpanded)
            {
                Rect foldoutRect = new Rect(position.x + foldoutX, position.y + lineHeight, position.width, lineHeight);

                EditorGUI.indentLevel = 1;

                // runtime value
                EditorGUI.PrefixLabel(foldoutRect, new GUIContent("Runtime Value"));
                foldoutRect.x += foldoutRightX;
                EditorGUI.LabelField(foldoutRect, intVariableSO.runtimeValue.ToString());
                
                // initial value
                foldoutRect.y += lineHeight;
                foldoutRect.x -= foldoutRightX;
                EditorGUI.PrefixLabel(foldoutRect, new GUIContent("Initial Value"));
                foldoutRect.x += foldoutRightX;
                intVariableSO.initialValue = EditorGUI.IntField(foldoutRect, intVariableSO.initialValue);

                // always positive
                foldoutRect.y += lineHeight;
                foldoutRect.x -= foldoutRightX;
                EditorGUI.PrefixLabel(foldoutRect, new GUIContent("Always Positive"));
                foldoutRect.x += foldoutRightX;
                intVariableSO.alwaysPositive = EditorGUI.Toggle(foldoutRect, intVariableSO.alwaysPositive);
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 0;
            lineHeight = EditorGUIUtility.singleLineHeight;
            float nonFoldoutHeight = lineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount-1);
            float foldoutHeight = lineHeight * lineCountFoldout + EditorGUIUtility.standardVerticalSpacing * (lineCountFoldout-1);
            if (property.isExpanded)
            {
                height = nonFoldoutHeight + foldoutHeight;
            }
            else
            {
                height = EditorGUIUtility.singleLineHeight;
            }
            return height;
        }
    }
}

#endif
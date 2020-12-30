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
        bool showFoldout = false;
        bool foldoutOpen = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            lineHeight = EditorGUIUtility.singleLineHeight;
            Rect line1 = new Rect(position.x, position.y, position.width, lineHeight);
            EditorGUI.BeginProperty (line1, label, property);
			EditorGUI.PropertyField(line1, property, label);	
			EditorGUI.EndProperty();
            
            // don't render the foldout of the scriptable object isn't set yet
            if (property.objectReferenceValue == null)
            {
                showFoldout = false;
                return;
            }
            else
            {
                showFoldout = true;
            }

            // Foldout
            IntVariableSO intVariableSO = EditorUtils.GetTargetObjectOfProperty(property) as IntVariableSO;
            Rect foldoutRect = new Rect(position.x + foldoutX, position.y + lineHeight, position.width, lineHeight);
            foldoutOpen = EditorGUI.Foldout(foldoutRect, this.foldoutOpen, "Int SO Settings");
            if (foldoutOpen)
            {
                EditorGUI.indentLevel = 1;

                // runtime value
                foldoutRect.y += lineHeight;
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
            if (foldoutOpen)
            {
                height = nonFoldoutHeight + foldoutHeight;
            }
            else
            {
                height = nonFoldoutHeight;
            }
            
            if (!showFoldout)
            {
                height = EditorGUIUtility.singleLineHeight;
            }
            return height;
        }
    }

}
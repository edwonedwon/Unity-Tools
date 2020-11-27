using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Edwon.UnityTools 
{
    public class MenuItems
    {
        [MenuItem("Tools/Edwon/Parent To Empty %&#p")]
        private static void ParentToEmpty()
        {
            GameObject child = Selection.activeGameObject;
            string childName = child.name;
            int childIndex = child.transform.GetSiblingIndex();
            GameObject parent = new GameObject(childName);
            child.name = childName + " child";
            parent.layer = child.layer;
            parent.transform.parent = child.transform.parent;
            parent.transform.SetSiblingIndex(childIndex);
            parent.transform.position = child.transform.position;
            parent.transform.rotation = child.transform.rotation;
            parent.transform.localScale = child.transform.localScale;
            child.transform.parent = parent.transform;
            Selection.activeGameObject = parent;
        }   

        [MenuItem("Tools/RemoveAllComponentsOfSelected")]
        static void RemoveAllComponentsOfSelected()
        {
            GameObject selected = Selection.activeGameObject;
            Component[] components = selected.GetComponents<Component>();
            foreach(Component c in components)
            {
                if (c.GetType() == typeof(Transform))
                    continue;

                if (Application.isPlaying)
                    Component.Destroy(c);
                else
                    Component.DestroyImmediate(c);
            }
        }

        [MenuItem("Tools/Edwon/Disable All Gizmos")]
        static void DisableAllGizmosMenu()
        {
            var Annotation = Type.GetType("UnityEditor.Annotation, UnityEditor");
            var ClassId = Annotation.GetField("classID");
            var ScriptClass = Annotation.GetField("scriptClass");
            
            Type AnnotationUtility = Type.GetType("UnityEditor.AnnotationUtility, UnityEditor");
            var GetAnnotations = AnnotationUtility.GetMethod("GetAnnotations", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var SetGizmoEnabled = AnnotationUtility.GetMethod("SetGizmoEnabled", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var SetIconEnabled = AnnotationUtility.GetMethod("SetIconEnabled", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            Array annotations = (Array)GetAnnotations.Invoke(null, null);
            foreach (var a in annotations)
            {
                int classId = (int)ClassId.GetValue(a);
                string scriptClass = (string)ScriptClass.GetValue(a);

                SetGizmoEnabled.Invoke(null, new object[] { classId, scriptClass, 0 });
                SetIconEnabled.Invoke(null, new object[] { classId, scriptClass, 0 });
            }
        }

        [MenuItem("Tools/Find Missing references in scene")]
        public static void FindMissingReferences()
        {
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

            foreach (var go in objects)
            {
                var components = go.GetComponents<Component>();

                foreach (var c in components)
                {
                    if (c == null)
                    {
                        Debug.LogError("Missing script found on: " + FullObjectPath(go));
                    }
                    else
                    {
                        SerializedObject so = new SerializedObject(c);
                        var sp = so.GetIterator();

                        while (sp.NextVisible(true))
                        {
                            if (sp.propertyType != SerializedPropertyType.ObjectReference)
                            {
                                continue;
                            }

                            if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                            {
                                ShowError(FullObjectPath(go), sp.name);
                            }
                        }
                    }
                }
            }
        }

        private static void ShowError(string objectName, string propertyName)
        {
            Debug.LogError("Missing reference found in: " + objectName + ", Property : " + propertyName);
        }

        private static string FullObjectPath(GameObject go)
        {
            return go.transform.parent == null ? go.name : FullObjectPath(go.transform.parent.gameObject) + "/" + go.name;
        }
    }
}
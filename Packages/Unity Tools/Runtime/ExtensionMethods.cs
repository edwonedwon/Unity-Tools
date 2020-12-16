using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Edwon.Tools 
{
    public static class ExtensionMethods
    {
        public static float Remap(this float value, float inputMin, float inputMax, float outputMin, float outputMax)
        {
            return (Mathf.Clamp(value, inputMin, inputMax) - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
        }

        public static int Remap(this int value, int inputMin, int inputMax, int outputMin, int outputMax)
        {
            return (int)(((float)(Mathf.Clamp(value, inputMin, inputMax)) - (float)inputMin) / ((float)inputMax - (float)inputMin) * ((float)outputMax - (float)outputMin) + (float)outputMin);
        }

        public static Vector3 LerpBetweenPoints(this Vector3 value, Vector3 start, Vector3 end, float percent)
        {
            return (start + percent * (end - start));
        }

        public static string SafeSubstring(this string text, int start, int length)
        {
            if (text == null) return null;

            return text.Length <= start ? ""
                : text.Length - start <= length ? text.Substring(start)
                : text.Substring(start, length);
        }

        /// <summary>
        /// Gets or add a component. Usage example:
        /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
        /// </summary>
        static public T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T result = child.GetComponent<T>();
            if (result == null)
            {
                result = child.gameObject.AddComponent<T>();
            }
            return result;
        }

        public static T GetComponentInSelfOrParent<T>(this Component component) where T: Component
        {
            T result = component.GetComponent<T>();
            if (result == null) {
                result = component.GetComponentInParent<T>();
            }
            return result;
        }

        public static void EnableChildRenderers(this Transform parent, bool enabled, bool includeParent = true, List<Transform> exceptions = null)
        {
            Renderer[] components = parent.GetComponentsInChildren<Renderer>();
            foreach(Renderer component in components)
            {
                // include parent
                if (includeParent)
                {
                    if (exceptions == null)
                    {
                        component.enabled = enabled;
                    }
                    // if in the exceptions list
                    else 
                    {
                        if (!exceptions.Contains(component.transform))
                        {
                            component.enabled = enabled;
                        }
                    }
                }
                // don't include parent
                else
                {
                    if (component.transform != parent)
                    {
                        component.enabled = enabled;
                    }
                }
            }
        }

        public static void EnableChildColliders(this Transform parent, bool enabled, bool includeParent = true, List<Transform> exceptions = null)
        {
            Collider[] components = parent.GetComponentsInChildren<Collider>();
            foreach (Collider component in components)
            {
                if (includeParent)
                {
                    // if no exceptions
                    if (exceptions == null)
                    {
                        // toggle
                        component.enabled = enabled;
                    }
                    // if exceptions
                    else
                    {
                        // if not in the list
                        if (!exceptions.Contains(component.transform))
                        {
                            // toggle
                            component.enabled = enabled;
                        }
                    }
                }
                // don't include parent
                else
                {
                    if (component.transform != parent)
                    {
                        component.enabled = enabled;
                    }
                }
            }
        }

        public static void EnableChildLights(this Transform parent, bool enabled, bool includeParent = true, List<Transform> exceptions = null)
        {
            Light[] components = parent.GetComponentsInChildren<Light>();
            foreach (Light component in components)
            {
                if (includeParent)
                {
                    // if no exceptions
                    if (exceptions == null)
                    {
                        // toggle
                        component.enabled = enabled;
                    }
                    // if exceptions
                    else
                    {
                        // if not in the list
                        if (!exceptions.Contains(component.transform))
                        {
                            // toggle
                            component.enabled = enabled;
                        }
                    }
                }
                // don't include parent
                else
                {
                    if (component.transform != parent)
                    {
                        component.enabled = enabled;
                    }
                }
            }
        }

        public static void SetLayerRecursive(this GameObject obj, int newLayer)
        {
            obj.layer = newLayer;

            foreach (Transform child in obj.GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = newLayer;
            }
        }

        public static bool ContainsAny(this string value, params string[] strings)
        {
            foreach (string s in strings)
            {
                if (value.Contains(s))
                    return true;
            }

            return false;
        }

        public static bool ContainsAll(this string value, params string[] strings)
        {
            return strings.All(x => value.Contains(x));
        }

        public static bool IsWhiteSpaceOnly(this string value)
        {
            bool isWhiteSpaceOnly = true;
            foreach(Char c in value)
            {
                if (Char.IsWhiteSpace(c) == false)
                {
                    isWhiteSpaceOnly = false;
                }
            }
            return isWhiteSpaceOnly;
        }
    }
}
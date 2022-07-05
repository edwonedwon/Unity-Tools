
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;
using System.Linq;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Edwon.Tools 
{
    public static class Utils
    {
        public static bool CheckNear(Vector3 start, Vector3 target, float targetDistance)
        {
            return ((target - start).sqrMagnitude < targetDistance * targetDistance);
        }

        public static Vector3 GetCenterPoint(List<Transform> gos)
        {
            if (gos.Count == 0)
                return Vector3.zero;

            if (gos.Count == 1)
                return gos[0].transform.position;

            Bounds bounds = new Bounds(gos[0].transform.position, Vector3.zero);
            for (var i = 1; i < gos.Count; i++)
                bounds.Encapsulate(gos[i].transform.position);
            return bounds.center;
        }

        public static Vector3 QuickVector(float setAllDimensionsTo)
        {
            return new Vector3(setAllDimensionsTo, setAllDimensionsTo, setAllDimensionsTo);
        }

        private static double Distance(Vector3 p1, Vector3 p2)
        {
            return Mathf.Sqrt(
                Mathf.Pow(p2.x - p1.x, 2) + 
                Mathf.Pow(p2.y - p1.y, 2) + 
                Mathf.Pow(p2.z - p1.z, 2));
        }

        private static double DistanceQuick(Vector3 p1, Vector3 p2)
        {
            // distance will this or less
            double deltaX = Mathf.Abs(p2.x - p1.x);
            double deltaY = Mathf.Abs(p2.y - p1.y);
            double deltaZ = Mathf.Abs(p2.z - p1.z);

            double deltaToReturn = 0;
            if (deltaX > deltaY)
                deltaToReturn = deltaX;
            else
                deltaToReturn = deltaY;

            return deltaToReturn;
        }  

        public static List<Vector3> OrderByDistance(List<Vector3> pointList)
        {
            var orderedList = new List<Vector3>();
            var currentPoint = pointList[0];
            while (pointList.Count > 1)
            {
                orderedList.Add(currentPoint);
                pointList.RemoveAt(pointList.IndexOf(currentPoint));
                var closestPointIndex = 0;
                var closestDistance = double.MaxValue;
                for (var i = 0; i < pointList.Count; i++)
                {
                    var distanceQuick = DistanceQuick(currentPoint, pointList[i]);
                    if(distanceQuick > closestDistance)
                        continue;
                    var distance = Distance(currentPoint, pointList[i]);
                    if (distance < closestDistance)
                    {
                        closestPointIndex = i;
                        closestDistance = distance;
                    }
                }    
                currentPoint = pointList[closestPointIndex];
            }
            // Add the last point.
            orderedList.Add(currentPoint);
            return orderedList;
        }  

        public static string UniqueID()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
            int z1 = UnityEngine.Random.Range(0, 1000000);
            int z2 = UnityEngine.Random.Range(0, 1000000);
            string uid = currentEpochTime + ":" + z1 + ":" + z2;
            return uid;
        }

        static IEnumerator Poop()
        {
            yield return null;
        }

        public static string RemoveParenthesisAndInside(string toRemoveFrom)
        {
            int firstBracket = toRemoveFrom.IndexOf('(');
            int lastBracket = toRemoveFrom.LastIndexOf(')');
            if (firstBracket > -1 && lastBracket > 0)
            {
                int diff = lastBracket - firstBracket + 1;
                string toReturn = toRemoveFrom.Remove(firstBracket, diff);
                return toReturn.Trim();
            }
            
            return toRemoveFrom;
        }

        public static int HowManyOtherGameObjectsWithSameName(string name, bool removeParenthesis)
        {
            int total = 0;
            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
            for (int i = gameObjects.Length-1; i >= 0; i--)
            {
                if (removeParenthesis)
                {
                    string gameObjectNameStripped = RemoveParenthesisAndInside(gameObjects[i].name);
                    if (name == gameObjectNameStripped)
                        total ++;
                }
                else
                    total++;
            }
            if (total > 0)
                total -= 1;
            return total;
        }

        public static void PlayerPrefsSetBool(string name, bool booleanValue)
        {
            PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
        }

        public static bool PlayerPrefsGetBool(string name)
        {
            return PlayerPrefs.GetInt(name) == 1 ? true : false;
        }

        public static bool PlayerPrefsGetBool(string name, bool defaultValue)
        {
            if (PlayerPrefs.HasKey(name))
            {
                return PlayerPrefsGetBool(name);
            }

            return defaultValue;
        }

        public static void ShowCanvasGroup(CanvasGroup cg, bool show, bool setActive = true, bool toggleInteractivity = true)
        {
            if (show)
            {
                if (setActive)
                    cg.gameObject.SetActive(true);

                cg.alpha = 1;
                if (toggleInteractivity)
                {
                    cg.interactable = true;
                    cg.blocksRaycasts = true;
                }
            }
            else
            {
                if (setActive)
                    cg.gameObject.SetActive(false);

                cg.alpha = 0;
                if (toggleInteractivity)
                {
                    cg.interactable = false;
                    cg.blocksRaycasts = false;
                }
            }
        }

        public static bool IsCanvasGroupVisible(CanvasGroup cg)
        {
            return (cg.alpha == 1) ? true : false;
        }

        public static void ScreenRect(out int width, out int height, int maxWidth)
        {
            width = Screen.width;
            height = Screen.height;

            if (width > maxWidth)
                width = maxWidth;
            else if (maxWidth > width)
                maxWidth = width;

            if (width % 2 == 1)
                width++;

            float ratio = (float)maxWidth / (float)Screen.width;
            height = (int)(Screen.height * ratio);

            if (height % 2 == 1)
                height++;

            // Debug.Log("new width/height = " + width + " x " + height);
        }

        public static void PlayVideoAtPath(VideoPlayer videoPlayer, string path)
        {
            if (!Application.isEditor) // if phone, play video at path
            {
                Debug.Log("playing video file at: " + path);
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url =  "file://" + path;
                videoPlayer.Play();
            }
            else // if editor play test video
            {
                Debug.Log("playing video file at: " + path);
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url =  path;
                videoPlayer.Play();
            }
        }

        public static void ToggleColliders(Collider[] components, bool toggle)
        {
            foreach(Collider c in components)
            {
                c.enabled = toggle;
            }
        }

        public static void ToggleColliders(Collider[] components, bool toggle, LayerMask excludeChecked)
        {
            foreach(Collider c in components)
            {
                if (Utils.IsLayerIncluded(c.gameObject.layer, excludeChecked))
                    continue;
                else
                    c.enabled = toggle;
            }
        }

        public static void UnfoldInEditorHierarchy(Transform transformParentToUnfold)
        {
            #if UNITY_EDITOR
            if (transformParentToUnfold.childCount > 0)
                UnityEditor.EditorGUIUtility.PingObject(transformParentToUnfold.GetChild(0));
            #endif
        }
        
        public static bool IsLayerIncluded(int layer, LayerMask layerMask)
        {
            if(((1<<layer) & layerMask) != 0)
                return true;
            else
                return false;
        }

        public static void EnableBuildDefine(bool enabled, string define)
        {
            #if UNITY_EDITOR
            string defines = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            List<string> definesList = defines.Split(new char[] { ';' }).ToList<string>();
            if (enabled)
            {
                // add define to defines
                if (!definesList.Contains(define))
                {
                    definesList.Add(define);
                }
            }
            else
            {
                // remove define from defines
                for (int i = definesList.Count - 1; i >= 0; i--)
                {
                    if (definesList[i] == define)
                    {
                        definesList.RemoveAt(i);
                    }
                }
            }

            defines = String.Join(";", definesList.ToArray());

            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
        #endif
        }

        public static bool EventHasTarget(UnityEvent e)
        {
            for (int i = 0; i < e.GetPersistentEventCount(); i++)
            {
                if (e.GetPersistentTarget(i) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EventHasTarget<T>(UnityEventEdwonBase<T> e)
        {
            for (int i = 0; i < e.GetPersistentEventCount(); i++)
            {
                if (e.GetPersistentTarget(i) != null)
                {
                    return true;
                }
            }
            return false;
        }


        public static Rigidbody AddGetRigidbody(Transform transform)
		{
			if (transform.GetComponent<Rigidbody>() == null)
			{
				return transform.gameObject.AddComponent<Rigidbody>();
			}
			else
			{
				return transform.GetComponent<Rigidbody>();
			}
		}

		public static RigidBodySettings GetRigidbodySettings (Rigidbody rb)
		{
			RigidBodySettings settings = new RigidBodySettings();
			settings.mass = rb.mass;
			settings.drag = rb.drag;
			settings.angularDrag = rb.angularDrag;
			settings.gravity = rb.useGravity;
			settings.kinematic = rb.isKinematic;
			settings.interpolation = rb.interpolation;
			settings.collisionDetection = rb.collisionDetectionMode;
			settings.constraints = rb.constraints;
			settings.sleepThreshold = rb.sleepThreshold;
			return settings;
		}

		public static void SetRigidbodySettings(Rigidbody rb, RigidBodySettings settings)
		{
			rb.mass = settings.mass;
			rb.drag = settings.drag;
			rb.angularDrag = settings.angularDrag;
			rb.useGravity = settings.gravity;
			rb.isKinematic = settings.kinematic;
			rb.interpolation = settings.interpolation;
			rb.collisionDetectionMode = settings.collisionDetection;
			rb.constraints = settings.constraints;
			rb.sleepThreshold = settings.sleepThreshold;
		}
		
		public static SpringSettings GetSpringSettings(SpringJoint spring)
		{
			SpringSettings settings = new SpringSettings();
			settings.power = spring.spring;
			settings.damper = spring.damper;
			settings.tolerance = spring.tolerance;
			settings.anchor = spring.anchor;
			settings.connectedAnchor = spring.connectedAnchor;
			settings.autoAnchor = spring.autoConfigureConnectedAnchor;
			return settings;
		}

		public static void SetSpringSettings(SpringJoint spring, SpringSettings settings)
		{
			spring.spring = settings.power;
			spring.damper = settings.damper;
			spring.tolerance = settings.tolerance;
			spring.anchor = settings.anchor;
			spring.autoConfigureConnectedAnchor = settings.autoAnchor;
			spring.connectedAnchor = settings.connectedAnchor;
		}

		public static TransformLocalSettings GetTransformLocalSettings(Transform tf)
		{
			TransformLocalSettings settings = new TransformLocalSettings();
			settings.localPosition = tf.localPosition;
			settings.localRotation = tf.localRotation;
			settings.localScale = tf.localScale;

			return settings;
		}

		public static void SetTransformLocalSettings(Transform tf, TransformLocalSettings settings)
		{
			tf.localPosition = settings.localPosition;
			tf.localRotation = settings.localRotation;
			tf.localScale = settings.localScale;
		}
    }
}
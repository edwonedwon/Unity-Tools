
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;
using System.Linq;

namespace Edwon.UnityTools 
{
    public static class Utils
    {
        public static string UniqueID()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
            int z1 = UnityEngine.Random.Range(0, 1000000);
            int z2 = UnityEngine.Random.Range(0, 1000000);
            string uid = currentEpochTime + ":" + z1 + ":" + z2;
            return uid;
        }
        public static Coroutine DoAfter(this MonoBehaviour mb, float delay, System.Action a)
        {
            return mb.StartCoroutine(mb.DoAfterCoroutine(delay, a));
        }

        static IEnumerator DoAfterCoroutine(this MonoBehaviour mb, float delay, System.Action a)
        {
            yield return new WaitForSeconds(delay);
            Debug.Log("did after: " + delay);
            a();
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
                // Debug.Log("playing video file at: " + path);
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url =  "file://" + path;
                videoPlayer.Play();
            }
            else // if editor play test video
            {
                videoPlayer.source = VideoSource.VideoClip;
                // videoPlayer.clip = VideoManager.Instance.testVideoClip;
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
    }
}
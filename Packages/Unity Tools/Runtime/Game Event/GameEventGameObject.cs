using UnityEngine;
using System.Collections.Generic;
using System;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event Game Object", menuName = "Scriptables/Game Event Game Object")]
    public class GameEventGameObject : ScriptableObject
    {
        private List<Action<GameObject>> listenerMethods = new List<Action<GameObject>>();

        public void Raise(GameObject go)
        {
            for(int i = 0; i < listenerMethods.Count; i++)
                listenerMethods[i](go);
        }

        public void RegisterListener(Action<GameObject> listener)
        { 
            listenerMethods.Add(listener); 
        }

        public void UnegisterListener(Action<GameObject> listener)
        { 
            listenerMethods.Remove(listener); 
        }
    }
}
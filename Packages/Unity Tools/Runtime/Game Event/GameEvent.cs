using UnityEngine;
using System.Collections.Generic;
using System;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event", menuName = "Scriptables/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<Action> listenerMethods = new List<Action>();

        public void Raise()
        {
            for(int i = 0; i < listenerMethods.Count; i++)
                listenerMethods[i]();
        }

        public void RegisterListener(Action listener)
        { 
            listenerMethods.Add(listener); 
        }

        public void UnregisterListener(Action listener)
        { 
            listenerMethods.Remove(listener); 
        }

    }
}
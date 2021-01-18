using UnityEngine;
using System.Collections.Generic;
using System;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event String", menuName = "Scriptables/Game Event String")]
    public class GameEventString: ScriptableObject
    {
        private List<Action<string>> listenerMethods = new List<Action<string>>();

        public void Raise(string _string)
        {
            for(int i = 0; i < listenerMethods.Count; i++)
                listenerMethods[i](_string);
        }

        public void RegisterListener(Action<string> listener)
        { 
            listenerMethods.Add(listener); 
        }

        public void UnregisterListener(Action<string> listener)
        { 
            listenerMethods.Remove(listener); 
        }

    }
}
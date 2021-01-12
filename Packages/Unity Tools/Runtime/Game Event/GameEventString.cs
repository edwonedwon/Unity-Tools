using UnityEngine;
using System.Collections.Generic;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event Game Object", menuName = "Scriptables/Game Event Game Object")]
    public class GameEventString: ScriptableObject
    {
        private List<IGameEventStringListener> listeners = new List<IGameEventStringListener>();

        public void Raise(string _string)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised(this, _string);
        }

        public void RegisterListener(IGameEventStringListener listener)
        { 
            listeners.Add(listener); 
        }

        public void UnregisterListener(IGameEventStringListener listener)
        { 
            listeners.Remove(listener); 
        }
    }
}
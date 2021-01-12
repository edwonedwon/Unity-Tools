using UnityEngine;
using System.Collections.Generic;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event", menuName = "Scriptables/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<IGameEventListener> listeners = new List<IGameEventListener>();

        public void Raise()
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised(this);
        }

        public void RegisterListener(IGameEventListener listener)
        { 
            listeners.Add(listener); 
        }

        public void UnregisterListener(IGameEventListener listener)
        { 
            listeners.Remove(listener); 
        }
    }
}
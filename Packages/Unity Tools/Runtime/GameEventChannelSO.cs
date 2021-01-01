using UnityEngine;
using System.Collections.Generic;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event", menuName = "Scriptables/Game Event")]
    public class GameEventChannelSO : ScriptableObject
    {
        public enum GameEventParameterType { None, String, GameObject }
        public GameEventParameterType parameterType;

        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void Raise(string s)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised(s);
        }

        public void Raise(GameObject gameObject)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised(gameObject);
        }

        public void RegisterListener(GameEventListener listener)
        { 
            listeners.Add(listener); 
        }

        public void UnregisterListener(GameEventListener listener)
        { 
            listeners.Remove(listener); 
        }
    }
}
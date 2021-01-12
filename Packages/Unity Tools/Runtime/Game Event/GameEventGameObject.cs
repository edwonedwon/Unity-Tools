using UnityEngine;
using System.Collections.Generic;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event Game Object", menuName = "Scriptables/Game Event Game Object")]
    public class GameEventGameObject : ScriptableObject
    {
        private List<IGameEventGameObjectListener> listeners = new List<IGameEventGameObjectListener>();

        public void Raise(GameObject _gameObject)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised(this, _gameObject);
        }

        public void RegisterListener(IGameEventGameObjectListener listener)
        { 
            listeners.Add(listener); 
        }

        public void UnregisterListener(IGameEventGameObjectListener listener)
        { 
            listeners.Remove(listener); 
        }
    }
}
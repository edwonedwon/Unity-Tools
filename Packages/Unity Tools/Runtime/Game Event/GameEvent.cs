using UnityEngine;
using System.Collections.Generic;
using System;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event", menuName = "Scriptables/Game Event")]
    public class GameEvent : ScriptableObject
    {
        public enum ParameterType { None, String, GameObject }
        public ParameterType parameterType;

        private List<Action> listeners = new List<Action>();
        private List<Action<string>> listenersString = new List<Action<string>>();
        private List<Action<GameObject>> listenersGameObject = new List<Action<GameObject>>();

        public void Raise()
        {
            if (IsParameterTypeDifferent(ParameterType.None)) {return;};
                
            for(int i = 0; i < listeners.Count; i++)
                listeners[i]();
        }

        public void Raise(string s)
        {
            if (IsParameterTypeDifferent(ParameterType.String)) {return;};

            for(int i = 0; i < listenersString.Count; i++)
                listenersString[i](s);
        }

        public void Raise(GameObject go)
        {
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            for(int i = 0; i < listenersGameObject.Count; i++)
                listenersGameObject[i](go);
        }

        public void RegisterListener(Action listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.None)) {return;};
            listeners.Add(listener); 
        }

        public void UnregisterListener(Action listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.None)) {return;};

            listeners.Remove(listener); 
        }

        public void RegisterListenerString(Action<string> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.String)) {return;};

            listenersString.Add(listener); 
        }

        public void UnregisterListenerString(Action<string> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.String)) {return;};

            listenersString.Remove(listener); 
        }

        public void RegisterListenerGameObject(Action<GameObject> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            listenersGameObject.Add(listener); 
        }

        public void UnregisterListenerGameObject(Action<GameObject> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            listenersGameObject.Remove(listener); 
        }

        public bool IsParameterTypeDifferent(ParameterType parameterType)
        {
            if (this.parameterType == parameterType)
            {
                return false;
            }
            else
            {
                Debug.Log(name + "'s parameter type is not " + parameterType);
                return true;
            }
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event", menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        public enum ParameterType { None, Bool, Int, Float, String, GameObject }
        public ParameterType parameterType;

        private List<Action> listeners = new List<Action>();
        private List<Action<bool>> listenersBool = new List<Action<bool>>();
        private List<Action<int>> listenersInt = new List<Action<int>>();
        private List<Action<float>> listenersFloat = new List<Action<float>>();
        private List<Action<string>> listenersString = new List<Action<string>>();
        private List<Action<GameObject>> listenersGameObject = new List<Action<GameObject>>();

        public void Raise()
        {
            if (IsParameterTypeDifferent(ParameterType.None)) {return;};
                
            for(int i = 0; i < listeners.Count; i++)
                listeners[i]();
        }

        public void Raise(bool value)
        {
            if (IsParameterTypeDifferent(ParameterType.Bool)) {return;};

            for(int i = 0; i < listenersBool.Count; i++)
                listenersBool[i](value);
        }

        public void Raise(int value)
        {
            if (IsParameterTypeDifferent(ParameterType.Int)) {return;};

            for(int i = 0; i < listenersInt.Count; i++)
                listenersInt[i](value);
        }

        public void Raise(float value)
        {
            if (IsParameterTypeDifferent(ParameterType.Float)) {return;};

            for(int i = 0; i < listenersFloat.Count; i++)
                listenersFloat[i](value);
        }

        public void Raise(string value)
        {
            if (IsParameterTypeDifferent(ParameterType.String)) {return;};

            for(int i = 0; i < listenersString.Count; i++)
                listenersString[i](value);
        }

        public void Raise(GameObject value)
        {
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            for(int i = 0; i < listenersGameObject.Count; i++)
                listenersGameObject[i](value);
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

        public void RegisterListenerBool(Action<bool> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Bool)) {return;};

            listenersBool.Add(listener); 
        }

        public void UnregisterListenerBool(Action<bool> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Bool)) {return;};

            listenersBool.Remove(listener); 
        }

        public void RegisterListenerInt(Action<int> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Int)) {return;};

            listenersInt.Add(listener); 
        }

        public void UnregisterListenerInt(Action<int> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Int)) {return;};

            listenersInt.Remove(listener); 
        }

        public void RegisterListenerFloat(Action<float> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Float)) {return;};

            listenersFloat.Add(listener); 
        }

        public void UnregisterListenerFloat(Action<float> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Float)) {return;};

            listenersFloat.Remove(listener); 
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
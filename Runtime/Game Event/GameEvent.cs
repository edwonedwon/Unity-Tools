using UnityEngine;
using System.Collections.Generic;
using System;

namespace Edwon.Tools
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Event", menuName = "Game Event")]
    public class GameEvent : ScopedScriptable
    {
        public enum ParameterType { None, Bool, Int, Float, String, AudioClip, Object, ObjectReturnBool, GameObject, ScriptableObject }
        public ParameterType parameterType;

        // events without returns
        private List<Action> listeners = new List<Action>();
        private List<Action<bool>> listenersBool = new List<Action<bool>>();
        private List<Action<int>> listenersInt = new List<Action<int>>();
        private List<Action<float>> listenersFloat = new List<Action<float>>();
        private List<Action<string>> listenersString = new List<Action<string>>();
        private List<Action<AudioClip>> listenersAudioClip = new List<Action<AudioClip>>();
        private List<Action<System.Object>> listenersObject = new List<Action<System.Object>>();
        private List<Action<GameObject>> listenersGameObject = new List<Action<GameObject>>();
        private List<Action<ScriptableObject>> listenersScriptableObject = new List<Action<ScriptableObject>>();
        
        // events with returns
        public delegate bool ActionObjectReturnBool(System.Object obj);
        public List<ActionObjectReturnBool> listenersObjectReturnBool = new List<ActionObjectReturnBool>();

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

        public void Raise(AudioClip value)
        {
            if (IsParameterTypeDifferent(ParameterType.AudioClip)) {return;};

            for(int i = 0; i < listenersAudioClip.Count; i++)
                listenersAudioClip[i](value);
        }

        public void Raise(System.Object value)
        {
            if (IsParameterTypeDifferent(ParameterType.Object)) {return;};

            for(int i = 0; i < listenersObject.Count; i++)
                listenersObject[i](value);
        }

        public bool RaiseAndReturn(System.Object value)
        {
            if (IsParameterTypeDifferent(ParameterType.ObjectReturnBool)) {return false;};

            // if any of the listeners returns true, this will return true
            bool returnValue = false;
            for(int i = 0; i < listenersObjectReturnBool.Count; i++)
            {
                returnValue = listenersObjectReturnBool[i](value);
            }
            return returnValue;
        }

        public void Raise(GameObject value)
        {
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            for(int i = 0; i < listenersGameObject.Count; i++)
                listenersGameObject[i](value);
        }

        public void Raise(ScriptableObject value)
        {
            if (IsParameterTypeDifferent(ParameterType.ScriptableObject)) {return;};

            for(int i = 0; i < listenersScriptableObject.Count; i++)
                listenersScriptableObject[i](value);
        }

        public void AddListener(Action listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.None)) {return;};
            listeners.Add(listener); 
        }

        public void RemoveListener(Action listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.None)) {return;};

            listeners.Remove(listener); 
        }

        public void AddListenerBool(Action<bool> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Bool)) {return;};

            listenersBool.Add(listener); 
        }

        public void RemoveListenerBool(Action<bool> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Bool)) {return;};

            listenersBool.Remove(listener); 
        }

        public void AddListenerInt(Action<int> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Int)) {return;};

            listenersInt.Add(listener); 
        }

        public void RemoveListenerInt(Action<int> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Int)) {return;};

            listenersInt.Remove(listener); 
        }

        public void AddListenerFloat(Action<float> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Float)) {return;};

            listenersFloat.Add(listener); 
        }

        public void RemoveListenerFloat(Action<float> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Float)) {return;};

            listenersFloat.Remove(listener); 
        }

        public void AddListenerString(Action<string> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.String)) {return;};

            listenersString.Add(listener); 
        }

        public void RemoveListenerString(Action<string> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.String)) {return;};

            listenersString.Remove(listener); 
        }

        public void AddListenerAudioClip(Action<AudioClip> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.AudioClip)) {return;};

            listenersAudioClip.Add(listener); 
        }

        public void RemoveListenerAudioClip(Action<AudioClip> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.AudioClip)) {return;};

            listenersAudioClip.Remove(listener); 
        }

        public void AddListenerObject(Action<System.Object> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Object)) {return;};

            listenersObject.Add(listener); 
        }

        public void RemoveListenerObject(Action<System.Object> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.Object)) {return;};

            listenersObject.Remove(listener); 
        }

        public void AddListenerObjectReturnBool(ActionObjectReturnBool listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.ObjectReturnBool)) {return;};

            listenersObjectReturnBool.Add(listener); 
        }

        public void RemoveListenerObjectReturnBool(ActionObjectReturnBool listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.ObjectReturnBool)) {return;};

            listenersObjectReturnBool.Add(listener); 
        }

        public void AddListenerGameObject(Action<GameObject> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            listenersGameObject.Add(listener); 
        }

        public void RemoveListenerGameObject(Action<GameObject> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.GameObject)) {return;};

            listenersGameObject.Remove(listener); 
        }

        public void AddListenerScriptableObject(Action<ScriptableObject> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.ScriptableObject)) {return;};

            listenersScriptableObject.Add(listener); 
        }

        public void RemoveListenerScriptableObject(Action<ScriptableObject> listener)
        { 
            if (IsParameterTypeDifferent(ParameterType.ScriptableObject)) {return;};

            listenersScriptableObject.Remove(listener); 
        }

        public bool IsParameterTypeDifferent(ParameterType parameterType)
        {
            if (this.parameterType == parameterType)
            {
                return false;
            }
            else
            {
                Debug.Log(name + "'s parameter type is not " + parameterType + "\n" +
                "use RegisterListenerFloat or RegisterListenerString etc... instead of just RegisterListener");
                return true;
            }
        }
    }
}
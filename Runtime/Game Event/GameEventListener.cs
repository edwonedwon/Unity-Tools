using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{
    public class UnityEventEdwonBase<T> : UnityEvent<T>{}
    [System.Serializable]
    public class UnityEventBool : UnityEventEdwonBase<bool>{}
    [System.Serializable]
    public class UnityEventInt : UnityEventEdwonBase<int>{}
    [System.Serializable]
    public class UnityEventFloat : UnityEventEdwonBase<float>{}
    [System.Serializable]
    public class UnityEventString : UnityEventEdwonBase<string>{}
    [System.Serializable]
    public class UnityEventVector3 : UnityEventEdwonBase<Vector3>{}
    [System.Serializable]
    public class UnityEventObject : UnityEventEdwonBase<System.Object>{}
    [System.Serializable]
    public class UnityEventGameObject : UnityEventEdwonBase<GameObject>{}
    [System.Serializable]
    public class UnityEventScriptableObject : UnityEventEdwonBase<ScriptableObject>{}
    [System.Serializable]
    public class UnityEventItem : UnityEventEdwonBase<Item>{}

    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        public UnityEventBool responseBool;
        public UnityEvent responseBoolTrue;
        public UnityEvent responseBoolFalse;
        public UnityEventInt responseInt;
        public UnityEventFloat responseFloat;
        public UnityEventString responseString;
        public UnityEventObject responseObject;
        public UnityEventGameObject responseGameObject;
        public UnityEventScriptableObject responseScriptableObject;

        private void OnEnable()
        { 
            switch(gameEvent.parameterType)
            {
                case GameEvent.ParameterType.None:
                    gameEvent.RegisterListener(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Bool:
                    gameEvent.RegisterListenerBool(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Int:
                    gameEvent.RegisterListenerInt(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Float:
                    gameEvent.RegisterListenerFloat(OnEventRaised); 
                break;
                case GameEvent.ParameterType.String:
                    gameEvent.RegisterListenerString(OnEventRaised);
                break;
                case GameEvent.ParameterType.Object:
                    gameEvent.RegisterListenerObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.GameObject:
                    gameEvent.RegisterListenerGameObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.ScriptableObject:
                    gameEvent.RegisterListenerScriptableObject(OnEventRaised); 
                break;
            }
        }

        private void OnDisable()
        { 
            switch(gameEvent.parameterType)
            {
                case GameEvent.ParameterType.None:
                    gameEvent.UnregisterListener(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Bool:
                    gameEvent.UnregisterListenerBool(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Int:
                    gameEvent.UnregisterListenerInt(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Float:
                    gameEvent.UnregisterListenerFloat(OnEventRaised); 
                break;
                case GameEvent.ParameterType.String:
                    gameEvent.UnregisterListenerString(OnEventRaised);
                break;
                case GameEvent.ParameterType.Object:
                    gameEvent.UnregisterListenerObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.GameObject:
                    gameEvent.UnregisterListenerGameObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.ScriptableObject:
                    gameEvent.UnregisterListenerScriptableObject(OnEventRaised); 
                break;
            }
        }

        public void OnEventRaised()
        { 
            response.Invoke(); 
        }

        public void OnEventRaised(bool value)
        {
            responseBool.Invoke(value);
            if (value)
                responseBoolTrue.Invoke();
            else
                responseBoolFalse.Invoke();
        }

        public void OnEventRaised(int value)
        {
            responseInt.Invoke(value);
        }

        public void OnEventRaised(float value)
        {
            responseFloat.Invoke(value);
        }

        public void OnEventRaised(string s)
        {
            responseString.Invoke(s);
        }

        public void OnEventRaised(System.Object o)
        {
            responseObject.Invoke(o);
        }

        public void OnEventRaised(GameObject go)
        {
            responseGameObject.Invoke(go);
        }
        
        public void OnEventRaised(ScriptableObject so)
        {
            responseScriptableObject.Invoke(so);
        }
    }
}


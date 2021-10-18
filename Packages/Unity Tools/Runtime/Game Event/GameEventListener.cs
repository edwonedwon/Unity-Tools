using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        public UnityEventBool responseBool;
        public UnityEventInt responseInt;
        public UnityEventFloat responseFloat;
        public UnityEventString responseString;
        public UnityEventGameObject responseGameObject;

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
                case GameEvent.ParameterType.GameObject:
                    gameEvent.RegisterListenerGameObject(OnEventRaised); 
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
                case GameEvent.ParameterType.GameObject:
                    gameEvent.UnregisterListenerGameObject(OnEventRaised); 
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

        public void OnEventRaised(GameObject go)
        {
            responseGameObject.Invoke(go);
        }
    }
}


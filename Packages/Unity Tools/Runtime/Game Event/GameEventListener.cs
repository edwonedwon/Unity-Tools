using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        public UnityEventString responseString;
        public UnityEventGameObject responseGameObject;

        private void OnEnable()
        { 
            switch(gameEvent.parameterType)
            {
                case GameEvent.ParameterType.None:
                    gameEvent.RegisterListener(OnEventRaised); 
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


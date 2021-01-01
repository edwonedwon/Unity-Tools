using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{
    
    public class GameEventListener : MonoBehaviour
    {
        public GameEventChannelSO gameEvent;
        public UnityEvent response;
        public UnityEventString responseString;
        public UnityEventGameObject responseGameObject;

        private void OnEnable()
        { 
            gameEvent.RegisterListener(this); 
        }

        private void OnDisable()
        { 
            gameEvent.UnregisterListener(this); 
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
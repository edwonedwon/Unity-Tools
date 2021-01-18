using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{   
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        
        public void OnEventRaised()
        {
            response.Invoke();
        }

        private void OnEnable()
        {
            if (gameEvent != null)
                gameEvent.RegisterListener(OnEventRaised);
        }

        private void OnDisable()
        { 
            if (gameEvent != null)
                gameEvent.UnregisterListener(OnEventRaised); 
        }
    }
}
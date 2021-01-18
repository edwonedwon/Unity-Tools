using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{   
    public class GameEventStringListener : MonoBehaviour
    {
        public GameEventString gameEvent;
        public UnityEventString response;
        
        public void OnEventRaised(string _string)
        { 
            response.Invoke(_string); 
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
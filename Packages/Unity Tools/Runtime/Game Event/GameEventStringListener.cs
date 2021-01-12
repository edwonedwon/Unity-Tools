using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{   
    public interface IGameEventStringListener
    {
        void OnEventRaised(GameEventString _event, string _string);
    }

    public class GameEventStringListener : MonoBehaviour, IGameEventStringListener
    {
        public GameEventString gameEvent;
        public UnityEventString response;
        
        public void OnEventRaised(GameEventString _event, string _string)
        { 
            response.Invoke(_string); 
        }

        private void OnEnable()
        { 
            if (gameEvent != null)
                gameEvent.RegisterListener(this); 
        }

        private void OnDisable()
        { 
            if (gameEvent != null)
                gameEvent.UnregisterListener(this); 
        }
    }
}
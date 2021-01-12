using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{   
    public interface IGameEventListener
    {
        void OnEventRaised(GameEvent _event);
    }

    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        
        public void OnEventRaised(GameEvent _event)
        { 
            response.Invoke(); 
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
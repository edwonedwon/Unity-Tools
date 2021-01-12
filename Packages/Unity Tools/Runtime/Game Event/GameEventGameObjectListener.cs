using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{   
    public interface IGameEventGameObjectListener
    {
        void OnEventRaised(GameEventGameObject _event, GameObject _gameObject);
    }

    public class GameEventGameObjectListener : MonoBehaviour, IGameEventGameObjectListener
    {
        public GameEventGameObject gameEvent;
        public UnityEventGameObject response;
        
        public void OnEventRaised(GameEventGameObject _event, GameObject _gameObject)
        { 
            response.Invoke(_gameObject); 
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
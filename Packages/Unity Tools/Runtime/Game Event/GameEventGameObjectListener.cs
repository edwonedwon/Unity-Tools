using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{   
    public class GameEventGameObjectListener : MonoBehaviour
    {
        public GameEventGameObject gameEvent;
        public UnityEventGameObject response;
        
        public void OnEventRaised(GameObject _gameObject)
        { 
            response.Invoke(_gameObject); 
        }

        private void OnEnable()
        { 
            if (gameEvent != null)
                gameEvent.RegisterListener(OnEventRaised); 
        }

        private void OnDisable()
        { 
            if (gameEvent != null)
                gameEvent.UnegisterListener(OnEventRaised);
        }
    }
}
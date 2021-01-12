using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class GameEventGameObjectSender : MonoBehaviour
    {
        public GameEventGameObject gameEvent;
        
        public void Raise(GameObject _gameObject)
        {
            gameEvent.Raise(_gameObject);
        }
    }
}

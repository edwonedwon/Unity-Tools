using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class GameEventSender : MonoBehaviour
    {
        public GameEventChannelSO gameEvent;
        
        public void Raise()
        {
            gameEvent.Raise();
        }

        public void Raise(string s)
        {
            gameEvent.Raise(s);
        }
        
        public void Raise(GameObject _gameObject)
        {
            gameEvent.Raise(_gameObject);
        }
    }
}

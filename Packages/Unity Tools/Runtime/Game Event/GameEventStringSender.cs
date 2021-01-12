using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class GameEventStringSender : MonoBehaviour
    {
        public GameEventString gameEvent;
        
        public void Raise(string _string)
        {
            gameEvent.Raise(_string);
        }
    }
}

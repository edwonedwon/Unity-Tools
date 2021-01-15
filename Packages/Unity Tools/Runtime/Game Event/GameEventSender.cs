using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class GameEventSender : MonoBehaviour
    {
        public void Raise(GameEvent gameEvent)
        {
            gameEvent.Raise();
        }
    }
}

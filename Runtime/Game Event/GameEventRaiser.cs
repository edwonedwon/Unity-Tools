using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    public class GameEventRaiser : MonoBehaviour
    {
        [InlineEditor]
        public GameEvent gameEvent;

        [Button]
        public void Raise()
        {
            gameEvent.Raise();
        }
    }
}
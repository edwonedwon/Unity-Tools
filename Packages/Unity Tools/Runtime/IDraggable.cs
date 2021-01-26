using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;

namespace Edwon.Tools
{
    public interface IDraggable
    {
        bool IsDragged {get;}
        void OnDragBegin(Vector2 screenPos);
        void OnDragUpdate(Vector2 screenPos);
        void OnDragEnd(Vector2 screenPos);
    }
}
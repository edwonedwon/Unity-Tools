using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools {
    
    // use this when interfacing with Lean Touch's LeanSelectable.cs 
    public interface ISelectable
    {
        void OnSelect(Vector2 screenPos);
        void OnSelectUpdate(Vector2 screenPos);
        void OnSelectUp(Vector2 screenPos);
        void OnDeselect();
    }
}
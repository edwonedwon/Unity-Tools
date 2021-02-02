using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public interface IHoldable
    {
        GameObject GameObject {get;}
        bool IsHeld {get;}
        bool SmoothMovement {get; set;}
        Holder holder {get; set;}
        Holder holderLast {get; set;}
        void Release(bool andDestroy = false);
        void OnHold(Holder holder);
        void OnRelease();
    }
}
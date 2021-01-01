using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Edwon.Tools 
{
    public class UIImageSetter : MonoBehaviour
    {
        public Image imageToSet;

        public void SetImageSprite(Sprite sprite)
        {
            imageToSet.sprite = sprite;
        }
    }
}

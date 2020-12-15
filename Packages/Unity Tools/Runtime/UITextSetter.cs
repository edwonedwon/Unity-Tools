using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Edwon.Tools
{
    [RequireComponent(typeof(Text))]
    public class UITextSetter : MonoBehaviour
    {
        Text text;

        private void Awake() 
        {
            text = GetComponent<Text>();    
        }

        public void SetText(string s)
        {
            text.text = s;
        }

        public void SetText(int i)
        {
            text.text = i.ToString();
        }
    }
}

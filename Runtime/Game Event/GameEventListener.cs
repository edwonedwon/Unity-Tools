using UnityEngine;
using UnityEngine.Events;
using Edwon.Tools;

namespace Edwon.Tools
{
    public class UnityEventEdwonBase<T> : UnityEvent<T>{}
    [System.Serializable]
    public class UnityEventBool : UnityEventEdwonBase<bool>{}
    [System.Serializable]
    public class UnityEventInt : UnityEventEdwonBase<int>{}
    [System.Serializable]
    public class UnityEventFloat : UnityEventEdwonBase<float>{}
    [System.Serializable]
    public class UnityEventString : UnityEventEdwonBase<string>{}
    [System.Serializable]
    public class UnityEventAudioClip : UnityEventEdwonBase<AudioClip>{}
    [System.Serializable]
    public class UnityEventAnimationClip : UnityEventEdwonBase<AnimationClip>{}
    [System.Serializable]
    public class UnityEventVector3 : UnityEventEdwonBase<Vector3>{}
    [System.Serializable]
    public class UnityEventObject : UnityEventEdwonBase<System.Object>{}
    [System.Serializable]
    public class UnityEventGameObject : UnityEventEdwonBase<GameObject>{}
    [System.Serializable]
    public class UnityEventScriptableObject : UnityEventEdwonBase<ScriptableObject>{}
    [System.Serializable]
    public class UnityEventItem : UnityEventEdwonBase<Item>{}

    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        public UnityEventBool responseBool;
        public UnityEvent responseBoolTrue;
        public UnityEvent responseBoolFalse;
        public UnityEventInt responseInt;
        public UnityEventFloat responseFloat;
        public UnityEventString responseString;
        public UnityEventAudioClip responseAudioClip;
        public UnityEventAnimationClip responseAnimationClip;
        public UnityEventObject responseObject;
        public UnityEventGameObject responseGameObject;
        public UnityEventScriptableObject responseScriptableObject;

        private void OnEnable()
        { 
            switch(gameEvent.parameterType)
            {
                case GameEvent.ParameterType.None:
                    gameEvent.AddListener(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Bool:
                    gameEvent.AddListenerBool(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Int:
                    gameEvent.AddListenerInt(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Float:
                    gameEvent.AddListenerFloat(OnEventRaised); 
                break;
                case GameEvent.ParameterType.String:
                    gameEvent.AddListenerString(OnEventRaised);
                break;
                case GameEvent.ParameterType.AudioClip:
                    gameEvent.AddListenerAudioClip(OnEventRaised);
                break;
                case GameEvent.ParameterType.AnimationClip:
                    gameEvent.AddListenerAnimationClip(OnEventRaised);
                break;
                case GameEvent.ParameterType.Object:
                    gameEvent.AddListenerObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.GameObject:
                    gameEvent.AddListenerGameObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.ScriptableObject:
                    gameEvent.AddListenerScriptableObject(OnEventRaised); 
                break;
            }
        }

        private void OnDisable()
        { 
            switch(gameEvent.parameterType)
            {
                case GameEvent.ParameterType.None:
                    gameEvent.RemoveListener(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Bool:
                    gameEvent.RemoveListenerBool(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Int:
                    gameEvent.RemoveListenerInt(OnEventRaised); 
                break;
                case GameEvent.ParameterType.Float:
                    gameEvent.RemoveListenerFloat(OnEventRaised); 
                break;
                case GameEvent.ParameterType.String:
                    gameEvent.RemoveListenerString(OnEventRaised);
                break;
                case GameEvent.ParameterType.AudioClip:
                    gameEvent.RemoveListenerAudioClip(OnEventRaised);
                break;
                case GameEvent.ParameterType.AnimationClip:
                    gameEvent.RemoveListenerAnimationClip(OnEventRaised);
                break;
                case GameEvent.ParameterType.Object:
                    gameEvent.RemoveListenerObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.GameObject:
                    gameEvent.RemoveListenerGameObject(OnEventRaised); 
                break;
                case GameEvent.ParameterType.ScriptableObject:
                    gameEvent.RemoveListenerScriptableObject(OnEventRaised); 
                break;
            }
        }

        public void OnEventRaised()
        { 
            response.Invoke(); 
        }

        public void OnEventRaised(bool value)
        {
            responseBool.Invoke(value);
            if (value)
                responseBoolTrue.Invoke();
            else
                responseBoolFalse.Invoke();
        }

        public void OnEventRaised(int value)
        {
            responseInt.Invoke(value);
        }

        public void OnEventRaised(float value)
        {
            responseFloat.Invoke(value);
        }

        public void OnEventRaised(string s)
        {
            responseString.Invoke(s);
        }
        
        public void OnEventRaised(AudioClip s)
        {
            responseAudioClip.Invoke(s);
        }
        
        public void OnEventRaised(AnimationClip s)
        {
            responseAnimationClip.Invoke(s);
        }

        public void OnEventRaised(System.Object o)
        {
            responseObject.Invoke(o);
        }

        public void OnEventRaised(GameObject go)
        {
            responseGameObject.Invoke(go);
        }
        
        public void OnEventRaised(ScriptableObject so)
        {
            responseScriptableObject.Invoke(so);
        }
    }
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
#if EDWON_DOTWEEN
using DG.Tweening;
#endif

public class UIPanelPopOut : MonoBehaviour
{
    bool isOpen = false;
    RectTransform rectTransform;
    public float openTime = .3f;
    #if EDWON_DOTWEEN
    public Ease openEase;
    public Ease closeEase;
    #endif
    public float closeTime = .7f;
    public Button dragButton;

    public UnityEvent onOpenComplete;
    public UnityEvent onCloseComplete;

    public enum PanelFromSide {Left, Right}
    public PanelFromSide side;
    Action<float> setAnchor;
    Action onComplete;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SetActions();
    }

    // set variables up based on panel side
    void SetActions()
    {
        if (side == PanelFromSide.Left)
        {
            setAnchor = (float x)=> rectTransform.anchorMax = new Vector2(x, 0);
        }
        else if (side == PanelFromSide.Right)
        {
            setAnchor = (float x) => rectTransform.anchorMin = new Vector2(x, 0);
        }
    }

    void DoTweenTo(Action<float> onUpdate, float xStartValue, float xEndValue, float duration, UnityEvent onComplete)
    {
        #if EDWON_DOTWEEN
        float tweenedValue = xStartValue;
        DOTween.To(() => tweenedValue, x => tweenedValue = x, xEndValue, openTime)
            .OnUpdate(() => onUpdate(tweenedValue))
            .SetEase(openEase)
            .OnComplete(() => onComplete.Invoke());
        #endif
    }

    [InspectorButton("Open")]
    public bool open;
    public void Open()
    {
        if (isOpen)
            return;

        isOpen = true;

        SetActions();

        float xStartValue = side == PanelFromSide.Left ? 0 : 1;
        float xEndValue = side == PanelFromSide.Left ? 1 : 0;

        if (Application.isPlaying) // if player
        {
            DoTweenTo(setAnchor, xStartValue, xEndValue, openTime, onOpenComplete);
        }
        else // if editor
        {
            rectTransform = GetComponent<RectTransform>();
            setAnchor(xEndValue);
        }
    }

    public void CloseAfterDelay(float delay)
    {
        #if EDWON_DOTWEEN
        DOVirtual.DelayedCall(delay, ()=>
        {
            Close();
        });
        #else
        Debug.LogWarning("the CloseAfterDelay only works if Dottween is installed and EDWON_DOTWEEN is set as a scripting define symbol in project settings");
        #endif
    }

    [InspectorButton("Close")]
    public bool close;
    public void Close()
    {
        if (!isOpen)
            return;

        isOpen = false;
        
        SetActions();

        float xEndValue = side == PanelFromSide.Left ? 0 : 1;
        float xStartValue = side == PanelFromSide.Left ? 1 : 0;

        if (Application.isPlaying) // if player
        {
            DoTweenTo(setAnchor, xStartValue, xEndValue, closeTime, onCloseComplete);
        }
        else // if editor
        {
            rectTransform = GetComponent<RectTransform>();
            setAnchor(xEndValue);
        }
        
    }
}

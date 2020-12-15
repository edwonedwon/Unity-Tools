using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class UIPanelPopOut : MonoBehaviour
{
    RectTransform rectTransform;
    public float openTime = .3f;
    public Ease openEase;
    public float closeTime = .7f;
    public Ease closeEase;
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
        float tweenedValue = xStartValue;
        DOTween.To(() => tweenedValue, x => tweenedValue = x, xEndValue, openTime)
            .OnUpdate(() => onUpdate(tweenedValue))
            .SetEase(openEase)
            .OnComplete(() => onComplete.Invoke());
    }

    [InspectorButton("Open")]
    public bool open;
    public void Open()
    {
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

    [InspectorButton("Close")]
    public bool close;
    public void Close()
    {
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

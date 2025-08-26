using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

[AddComponentMenu("Input/On-Screen Button")]
public class OnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{
    [InputControl(layout = "Button")]
    [SerializeField] private string m_ControlPath;
    [SerializeField] MyInputInit _InputControl;
    protected override string controlPathInternal
    {
        //get => m_ControlPath;
        //set => m_ControlPath = value;
        set { m_ControlPath = value; }
        get { return m_ControlPath; }
    }
    private void Awake()
    {

    }

    private void HandelTdTap(Vector2 arg0)
    {
        Debug.Log($"2d Tap: {arg0}");
    }

    public void HandleTap(float arg0)
    {
        //Debug.Log($"Handle Tap: {arg0}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"On Pointer Down: {eventData}");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log($"On Pointer Up: {eventData}");
    }
}
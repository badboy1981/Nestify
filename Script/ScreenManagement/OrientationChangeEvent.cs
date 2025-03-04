using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("My Asset/Screen Management"), fileName = "Screen Orientation")]
public class OrientationChangeEvent : ScriptableObject
{
    public event UnityAction<ScreenOrientation> OnOrientationChangedEvent = delegate { };

    private ScreenOrientation currentOrientation;
    public void Initialize()
    {
        currentOrientation = Screen.orientation;
    }
    public void CheckOrientationChange()
    {
        if (Screen.orientation != currentOrientation)
        {
            currentOrientation = Screen.orientation;
            OnOrientationChangedEvent?.Invoke(currentOrientation);
        }
    }
}
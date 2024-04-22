using NameInput001;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = ("My Asset/MazeControl"), fileName = "MazeControl")]
public class MyInputInit : ScriptableObject, Input001.IPlayerActions
{
    private Input001 _InputControll;
    private void OnEnable()
    {
        if (_InputControll == null)
        {
            _InputControll = new Input001();
            _InputControll.Player.SetCallbacks(this);
            SetGamePlay();
        }
    }
    private void SetGamePlay()
    {
        _InputControll.Player.Enable();
    }
    private void SetUI()
    {
        _InputControll.Player.Disable();
    }
    public event UnityAction<Vector2> MoveStartEvent = delegate { };
    public event UnityAction<float> MoveCancledEvent = delegate { };
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase.IsInProgress())
            MoveStartEvent?.Invoke(context.ReadValue<Vector2>());

        if (context.phase == InputActionPhase.Canceled)
            MoveCancledEvent?.Invoke(context.ReadValue<float>());
    }
}
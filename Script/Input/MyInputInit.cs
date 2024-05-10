using NameInput001;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = ("My Asset/MazeControl"), fileName = "MazeControl")]
public class MyInputInit : ScriptableObject, Input001.IPlayerActions,Input001.IMapActions
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
    private void OnDisable()
    {

    }
    private void SetGamePlay()
    {
        _InputControll.Player.Enable();
        _InputControll.Map.Disable();
    }
    private void SetUI()
    {
        _InputControll.Player.Disable();
    }

    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<float> JumpEvent = delegate { };

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke(context.ReadValue<float>());
    }
}
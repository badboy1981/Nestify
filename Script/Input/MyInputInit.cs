using NameInput001;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = ("My Asset/MazeControl"), fileName = "MazeControl")]
public class MyInputInit : ScriptableObject, Input001.IPlayerActions, Input001.IMapActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<float> JumpEvent = delegate { };
    public event UnityAction<float> GoToMap = delegate { };
    public event UnityAction<float> GoToPlay = delegate { };



    private Input001 _InputControll;

    private void OnEnable()
    {
        if (_InputControll == null)
        {
            _InputControll = new Input001();
            _InputControll.Player.SetCallbacks(this);
            _InputControll.Map.SetCallbacks(this);
        }
        SetGamePlay();

    }
    private void OnDisable()
    {
        if (_InputControll != null) { DisableAllInput(); }
    }
    private void SetGamePlay()
    {
        _InputControll.Player.Enable();
        _InputControll.Map.Disable();
    }
    private void SetUI()
    {
        _InputControll.Player.Disable();
        _InputControll.Map.Enable();
    }
    private void DisableAllInput()
    {
        _InputControll.Player.Disable();
        _InputControll.Map.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke(context.ReadValue<float>());
    }

    public void OnGoToPlay(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GoToPlay?.Invoke(context.ReadValue<float>());

            SetGamePlay();
        }
    }

    public void OnGoMap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GoToMap?.Invoke(context.ReadValue<float>());

            SetUI();
        }
    }
}
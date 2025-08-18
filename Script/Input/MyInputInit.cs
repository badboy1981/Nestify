using NameInput001;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = ("My Asset/MazeControl"), fileName = "MazeControl")]
public class MyInputInit : ScriptableObject, Input001.IPlayerActions, Input001.IMapActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2> MoveEventStart = delegate { };
    public event UnityAction<Vector2> MoveEventPerformed = delegate { };
    public event UnityAction<Vector2> MoveEventCanceled = delegate { };

    public event UnityAction<float> JumpEvent = delegate { };
    public event UnityAction<float> GoToMap = delegate { };
    public event UnityAction<float> GoToPlay = delegate { };

    public event UnityAction<Vector2> MobileSteeringWheelEvent = delegate { };
    public event UnityAction<Vector2> MobileMoveButtonEvent = delegate { };

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
        switch (context.phase)
        {
            case InputActionPhase.Started:
                MoveEventStart?.Invoke(context.ReadValue<Vector2>());
                break;
            case InputActionPhase.Performed:
                MoveEventPerformed?.Invoke(context.ReadValue<Vector2>());                
                break;
            case InputActionPhase.Canceled:
                MoveEventCanceled?.Invoke(context.ReadValue<Vector2>());
                break;
        }
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
            SetGamePlay();
            GoToPlay?.Invoke(context.ReadValue<float>());
        }
    }

    public void OnGoMap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SetUI();
            GoToMap?.Invoke(context.ReadValue<float>());
        }
    }

    public void OnMobileSteeringWheel(InputAction.CallbackContext context)
    {
        MobileSteeringWheelEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMobileMoveButton(InputAction.CallbackContext context)
    {
        MobileMoveButtonEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "My Asset/InputReader")]
public class InputReader : ScriptableObject, KeyControl.IPlayerActions, KeyControl.IUIActions
{
    //public GameObject gameObject;
    private KeyControl _keyControl;
    private void OnEnable()
    {
        if (_keyControl == null)
        {
            _keyControl = new KeyControl();
            _keyControl.Player.SetCallbacks(this);
            _keyControl.UI.SetCallbacks(this);
            SetGamePlay();
        }
    }
    private void OnDisable()
    {
        if (_keyControl != null) { }
    }
    void SetGamePlay()
    {
        _keyControl.Player.Enable();
        _keyControl.UI.Disable();
    }
    void SetUI()
    {
        _keyControl.Player.Disable();
        _keyControl.UI.Enable();
    }
    public event UnityAction<Vector2> InputDirectionEvent = delegate { };
    public event UnityAction<float> SideMoveEvent = delegate { };
    public event UnityAction<float> SpeedEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction ResumeEvent = delegate { };

    public event UnityAction<Vector2> MovePerformPhaseEvent = delegate { };
    public event UnityAction<float> MoveStartEvent = delegate { };
    public event UnityAction<float> MoveCancledEvent = delegate { };

    public event UnityAction<bool> MoveStateEvent = delegate { };

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log($"Phase: {context.phase}, Vlaue: {context.ReadValue<Vector2>()}");
        SideMoveEvent?.Invoke(context.ReadValue<Vector2>().x);
        SpeedEvent?.Invoke(context.ReadValue<Vector2>().y);
        InputDirectionEvent?.Invoke(context.ReadValue<Vector2>());

        if (context.phase.IsInProgress())
            MoveStartEvent?.Invoke(context.ReadValue<Vector2>().x);

        if (context.phase == InputActionPhase.Canceled)
            MoveCancledEvent?.Invoke(context.ReadValue<Vector2>().x);

        MoveStateEvent?.Invoke(context.phase.IsInProgress());
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGamePlay();
        }
    }
}
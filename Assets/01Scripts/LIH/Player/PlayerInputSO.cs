using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/InputSO")]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    public event Action DashEvent;
    public event Action<bool> AttackEvent;
    public event Action<Vector2> MouseMoveEvent;
    public event Action<bool> MovementEvent;
    
    public Vector2 InputDirection { get; private set; }
    public Vector2 MousePos { get; private set; }
    
    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
        _controls.UI.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        InputDirection = context.ReadValue<Vector2>().normalized;
        
        if (context.performed)
            MovementEvent?.Invoke(true);
        else if(context.canceled)
            MovementEvent?.Invoke(false);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke(true);
        else if (context.canceled)
            AttackEvent?.Invoke(false);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed)
            DashEvent?.Invoke();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        MousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        
        if (context.performed)
            MouseMoveEvent?.Invoke(MousePos);
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;


    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/InputSO")]
    public class PlayerInputSO : ScriptableObject, Input2.IPlayerActions, Input2.IUIActions
    {
        public event Action JumpEvent;
        public event Action AttackEvent;
        public event Action OpenMenuKeyEvent;
        public event Action TurnEndEvent;
        public Action<int> UnitSwapEvent;
        public event Action OnClickEnter;
        public event Action OnClickExit;
        public event Action OnClickEnter2;
        public event Action OnClickExit2;
        public event Action<float> OnMouseScroll;
        public event Action Jump;
        public event Action Dodge;
        public event Action Skill1;
        public event Action Skill2;
        public event Action<Vector2> Move;

    public Vector2 InputDirection {get; private set;}
    public Vector2 MoveDir { get; private set; }

    public Vector2 MousePos {get; private set;}
        private Input2 _controls;

    public bool IsRMBPressing = false, IsLMBPressing = false;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Input2();
                _controls.Player.SetCallbacks(this);
                _controls.UI.SetCallbacks(this);
            }
            _controls.Player.Enable();
            _controls.UI.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.UI.Disable();
        }

        
        public void SetPlayerInput(bool isEnable)
        {
            if(isEnable)
                _controls.Player.Enable();
            else
                _controls.Player.Disable();
        }

        public void OnChange(InputAction.CallbackContext context)
        {
            if(context.performed)
            TurnEndEvent?.Invoke();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

        public void OnMouseButton(InputAction.CallbackContext context)
        {
        if (context.performed)
        {
            OnClickEnter2?.Invoke();
            IsLMBPressing = true;
        }

        if (context.canceled)
        {
            OnClickExit2?.Invoke();
            IsLMBPressing = false;
        }
    }
    public void OnMouseButton2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnClickEnter2?.Invoke();
            IsRMBPressing = true;
        }

        if (context.canceled)
        {
            OnClickExit2?.Invoke();
            IsRMBPressing = false;
        }

    }

    public void OnSwap(InputAction.CallbackContext context)
        {
            if(context.performed)
                UnitSwapEvent?.Invoke(context.ReadValue<int>());
        }

    public void OnScrol(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnMouseScroll?.Invoke(context.ReadValue<float>());
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.performed)
            Skill1?.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.performed)
            Skill2?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            Skill1?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
            Dodge?.Invoke();
    }

    public void OnEsc(InputAction.CallbackContext context)
        {
            if (context.performed)
                OpenMenuKeyEvent?.Invoke();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        void Input2.IUIActions.OnClick(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnOpenMenu(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDir = context.ReadValue<Vector2>();
    }
}

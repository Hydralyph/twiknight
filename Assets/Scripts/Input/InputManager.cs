using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "TwiKnight", fileName ="InputManager")]
public class InputManager : ScriptableObject, ControlScheme.IGameplayActions, ControlScheme.IUserInterfaceActions
{
    private ControlScheme _controlScheme;

    private void OnEnable()
    {
        if(_controlScheme == null)
        {
            _controlScheme = new ControlScheme();

            _controlScheme.Gameplay.SetCallbacks(this);
            _controlScheme.UserInterface.SetCallbacks(this);

            SetGameplayMapActive();
        }
    }

    public void SetGameplayMapActive()
    {
        _controlScheme.UserInterface.Disable();
        _controlScheme.Gameplay.Enable();
    }

    public void SetUserInterfaceMapActive()
    {
        _controlScheme.Gameplay.Disable();
        _controlScheme.UserInterface.Enable();
    }

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action JumpCancelledEvent;
    public event Action InteractEvent;
    public event Action AttackEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractEvent?.Invoke();
    } // ADD AN INTERACT CANCELLED EVENT

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            JumpCancelledEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Canceled)
        AttackEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUserInterfaceMapActive();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplayMapActive();
        }
    }
}

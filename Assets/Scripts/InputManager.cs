using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions
{
    public static bool disableInputs = false;

    public Vector2 MovementValue { get; private set; }
    public bool isPrimaryAbilityHeld;
    public event Action SecondaryAbilityEvent;
    public event Action SwitchLeftEvent;
    public event Action SwitchRightEvent;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (disableInputs)
        {
            MovementValue = Vector2.zero;
            return;
        }

        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnPrimaryAbility(InputAction.CallbackContext context)
    {
        if (disableInputs)
        {
            isPrimaryAbilityHeld = false;
            return;
        }

        if (context.performed)
        {
            isPrimaryAbilityHeld = true;
        }
        else if (context.canceled)
        {
            isPrimaryAbilityHeld = false;
        }
    }

    public void OnSecondaryAbility(InputAction.CallbackContext context)
    {
        if (disableInputs || !context.performed)
        {
            return;
        }

        SecondaryAbilityEvent?.Invoke();
    }

    public void OnSwitchLeft(InputAction.CallbackContext context)
    {
        if (disableInputs || !context.performed)
        {
            return;
        }

        SwitchLeftEvent?.Invoke();
    }

    public void OnSwitchRight(InputAction.CallbackContext context)
    {
        if (disableInputs || !context.performed)
        {
            return;
        }

        SwitchRightEvent?.Invoke();
    }
}

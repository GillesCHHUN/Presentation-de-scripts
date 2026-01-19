using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 RotationValue { get; private set; }

    public bool IsShooting { get; private set; }

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
            IsShooting  = true;
        
        if(context.canceled)
            IsShooting = false;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        RotationValue = context.ReadValue<Vector2>();
    }
}
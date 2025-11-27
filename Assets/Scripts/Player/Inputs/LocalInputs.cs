using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInputs : NetworkBehaviour
{
    public static LocalInputs Instance { get; private set; }

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode dashKey = KeyCode.E;

    public NetworkBool isJumpPressed;
    public NetworkBool isFirePressed;
    public NetworkBool isRunPressed;
    public NetworkBool isDashPressed;

    NetworkInputData _inputData;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            _inputData = new NetworkInputData();
            Instance = this;
            return;
        }

        enabled = false;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    isJumpPressed = true;
        //}

        isJumpPressed |= Input.GetKeyDown(jumpKey);

        isRunPressed |= Input.GetKeyDown(sprintKey);

        isDashPressed |= Input.GetKeyDown(dashKey);

        isFirePressed |= Input.GetKeyDown(KeyCode.Mouse0);

    }

    public NetworkInputData SetInputs()
    {
        _inputData.movementInput.x = Input.GetAxis("Horizontal");
        _inputData.movementInput.y = Input.GetAxis("Vertical");

        _inputData.buttons.Set(ButtonTypes.Jump, isJumpPressed);
        isJumpPressed = false;

        _inputData.buttons.Set(ButtonTypes.Run, isRunPressed);
        isRunPressed = false;

        _inputData.buttons.Set(ButtonTypes.Dash, isDashPressed);
        isDashPressed = false;

        _inputData.buttons.Set(ButtonTypes.Shot, isFirePressed);
        isFirePressed = false;

        return _inputData;
    }
}

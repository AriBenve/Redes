using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 movementInput;

    //public NetworkBool isFirePressed;
    //public NetworkBool isJumpPressed;

    //public const byte JUMP = 1;
    //public const byte SHOT = 2;

    public NetworkButtons buttons;
}

public enum ButtonTypes
{
    Jump = 0,
    Shot = 1,
    Dash = 2,
    Run = 3
}
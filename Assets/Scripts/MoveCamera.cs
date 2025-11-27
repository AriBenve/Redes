using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MoveCamera : NetworkBehaviour
{
    public NetworkTransform cameraPosition;
    
    public override void FixedUpdateNetwork() {
        transform.position = cameraPosition.transform.position;
    }
}

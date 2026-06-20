using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update(){
        transform.position = cameraPosition.transform.position;
    }

    public void SetPlayer(Transform camPos)
    {
        cameraPosition = camPos;
    }
}

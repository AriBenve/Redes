using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkMecanimAnimator))]
public class MovementAnimation : MonoBehaviour
{
    NetworkMecanimAnimator _mecanim;

    private void Awake()
    {
        _mecanim = GetComponent<NetworkMecanimAnimator>();

        var playerMovement = GetComponentInParent<PlayerMovement>(true);

        //playerMovement.OnMovement += UpdateAnimation;
    }

    void UpdateAnimation(float xVelocity)
    {
        _mecanim.Animator.SetFloat("xAxi", Mathf.Abs(xVelocity));
    }
}

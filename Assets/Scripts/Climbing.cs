using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;
    public PlayerMovement pm;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float _climbTimer;

    private bool _climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallAngle;
    private float _wallLookAngle;

    private RaycastHit _frontWallHit;
    private bool _wallFront;

    private void Update()
    {
        WallCheck();
        StateMachine();

        if(_climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        if(_wallFront && Input.GetKey(KeyCode.W) && _wallLookAngle < maxWallAngle)
        {
            if(!_climbing && _climbTimer > 0) StartClimbing();

            if(_climbTimer > 0) _climbTimer -= Time.deltaTime;
            if(_climbTimer < 0) StopClimbing();
        }
        else
        {
            if (_climbing) StopClimbing();
        }
    }

    private void WallCheck()
    {
        _wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out _frontWallHit, detectionLength, whatIsWall);
        _wallLookAngle = Vector3.Angle(orientation.forward, -_frontWallHit.normal);

        if(pm.grounded)
        {
            _climbTimer = maxClimbTime;

        }
        
    }

    private void StartClimbing()
    {
        _climbing = true;
    }

    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }

    private void StopClimbing()
    {
        _climbing = false;
    }
}

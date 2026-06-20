using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

public class Dashing : NetworkBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private NetworkRigidbody3D rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("Audio")]
    public List<AudioClip> audioClipList = new List<AudioClip>();
    public AudioSource audioSource;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float dashFov;
    [SerializeField] ParticleSystem _fowardDashParticleSystem;
    [SerializeField] ParticleSystem _backwardsDashParticleSystem;
    [SerializeField] ParticleSystem _leftDashParticleSystem;
    [SerializeField] ParticleSystem _rightDashParticleSystem;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    public override void Spawned()
    {
        if(HasInputAuthority)
            cam = FindObjectOfType<PlayerCam>();
        
        _fowardDashParticleSystem = GameObject.Find("ForwardDashParticle").GetComponent<ParticleSystem>();
        _backwardsDashParticleSystem = GameObject.Find("BackwardDashParticle").GetComponent<ParticleSystem>();
        _leftDashParticleSystem = GameObject.Find("LeftDashParticle").GetComponent<ParticleSystem>();
        _rightDashParticleSystem = GameObject.Find("RightDashParticle").GetComponent<ParticleSystem>();

        rb = GetComponent<NetworkRigidbody3D>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
            

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        pm.maxYspeed = maxDashYSpeed;

        int i = Random.Range(0, audioClipList.Count);
        audioSource.PlayOneShot(audioClipList[i], 0.7f);
        cam.DoFov(dashFov);
        PlayDashParticles();

        Transform forwardT;

        if (useCameraForward)
            forwardT = playerCam; /// where you're looking
        else
            forwardT = orientation; /// where you're facing (no up or down)

        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
            rb.Rigidbody.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        if (resetVel)
            rb.Rigidbody.velocity = Vector3.zero;

        rb.Rigidbody.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;
        pm.maxYspeed = 0;

        cam.DoFov(85f);

        if (disableGravity)
            rb.Rigidbody.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }

    private void PlayDashParticles()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        Vector3 _inputVector = new Vector3(_horizontal, 0f, _vertical);

        if(_inputVector.z > 0 && Mathf.Abs(_inputVector.x) <= _inputVector.z)
        {
            _fowardDashParticleSystem.Play();
            return;
        }
        if (_inputVector.z < 0 && Mathf.Abs(_inputVector.x) <= Mathf.Abs(_inputVector.z))
        {
            _backwardsDashParticleSystem.Play();
            return;
        }
        if (_inputVector.x > 0)
        {
            _rightDashParticleSystem.Play();
            return;
        }
        if (_inputVector.x < 0)
        {
            _leftDashParticleSystem.Play();
            return;
        }

        _fowardDashParticleSystem.Play();
    }
}


using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayShotParticles : NetworkBehaviour
{
    ParticleSystem _shotParticles;

    [Networked, OnChangedRender(nameof(PlayParticles))]
    NetworkBool HasShot { get; set; }

    private void Awake()
    {
        _shotParticles = GetComponent<ParticleSystem>();

        var weaponComponent = GetComponentInParent<PlayerWeapon>(true);
        weaponComponent.OnShot += () => HasShot = !HasShot;

    }

    void PlayParticles()
    {
        _shotParticles.Play();
    }
}

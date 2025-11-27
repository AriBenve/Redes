using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    [SerializeField] sbyte _maxHealth;

    [Networked, OnChangedRender(nameof(LifeChanged))]
    sbyte CurrentHealth { get; set; }

    [SerializeField] byte _currentRespawns;

    [SerializeField] float _respawnCooldown;

    TickTimer _respawnTimer;

    [Networked, OnChangedRender(nameof(DeadStateChanged))] 
    NetworkBool IsDead { get; set; }

    public event Action<bool> OnDeadUpdate;

    public override void Spawned()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(sbyte damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth > 0) return;

        if (_currentRespawns == 0)
        {
            DisconnectPlayer();
            return;
        }

        _currentRespawns--;

        _respawnTimer = TickTimer.CreateFromSeconds(Runner, _respawnCooldown);

        IsDead = true;
    }

    public override void FixedUpdateNetwork()
    {
        if (!_respawnTimer.Expired(Runner)) return;

        _respawnTimer = TickTimer.None;
        CurrentHealth = _maxHealth;

        IsDead = false;
    }

    void DeadStateChanged()
    {
        //Para que el raycast ignore al jugador muerto
        GetComponent<HitboxRoot>().HitboxRootActive = !IsDead;

        OnDeadUpdate(IsDead);
    }

    void LifeChanged()
    {
        //Llamar a la barra de vida pasandole la cantidad actual
    }

    void DisconnectPlayer()
    {
        //Si el jugador que murio no es el host, desconectarlo
        if (!Object.HasInputAuthority)
        {
            Runner.Disconnect(Object.InputAuthority);
        }
        //Despawnear el objeto
        Runner.Despawn(Object);
    }
}

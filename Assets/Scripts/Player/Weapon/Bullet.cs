using Fusion;
using Fusion.Addons.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody3D))]
public class Bullet : NetworkBehaviour
{
    [SerializeField] float _initialImpulse;
    [SerializeField] sbyte _damage;
    [SerializeField] float _lifeTime;

    TickTimer _lifeTimer;

    public override void Spawned()
    {
        GetComponent<Rigidbody>().AddForce(transform.right * _initialImpulse, ForceMode.VelocityChange);

        _lifeTimer = TickTimer.CreateFromSeconds(Runner, _lifeTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (!_lifeTimer.Expired(Runner)) return;

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority) return;

        if (other.TryGetComponent(out HealthComponent enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }

        Runner.Despawn(Object);
    }
}

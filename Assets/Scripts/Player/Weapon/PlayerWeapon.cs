using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] NetworkPrefabRef _bulletPrefab;
    [SerializeField] Transform _spawnPoint;

    public event Action OnShot;

    public void BulletShot()
    {
        if (!HasStateAuthority) return;

        Runner.Spawn(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);

        OnShot?.Invoke();
    }

    public void RaycastShot()
    {
        if (!HasStateAuthority) return;

        OnShot?.Invoke();

        var raycastBool = Runner.LagCompensation.Raycast(origin: transform.position,
                                                        direction: transform.right,
                                                        length: 100,
                                                        player: Object.InputAuthority,
                                                        hit: out var hitInfo);

        if (!raycastBool) return;

        if (hitInfo.Hitbox.Root.TryGetComponent(out HealthComponent enemyHealth))
        {
            enemyHealth.TakeDamage(25);
        }
    }
}

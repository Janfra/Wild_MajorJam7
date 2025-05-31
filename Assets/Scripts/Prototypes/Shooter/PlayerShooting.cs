using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour, IShooterHandler
{
    public event IShooterHandler.Shoot OnTryShoot;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private Transform _bulletOrigin;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private float _fireRate = 0.5f;
    public float FireRate => _fireRate;

    [SerializeField]
    private int _ammo;
    public int Ammo => _ammo;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Bullet _bulletPrefab;

    public BulletSpawnData[] BulletsSpawnData => new BulletSpawnData[]{ new BulletSpawnData(_bulletPrefab, transform.forward, _bulletOrigin.position, _bulletSpeed) };

    public Collider IgnoreCollider => _collider;

    private void Start()
    {
        if (_player == null)
        {
            _player = GetComponent<PlayerController>();
        }

        _player.AttackInput.started += TryShoot;
    }

    private void TryShoot(InputAction.CallbackContext context)
    {
        OnTryShoot?.Invoke();
    }
}

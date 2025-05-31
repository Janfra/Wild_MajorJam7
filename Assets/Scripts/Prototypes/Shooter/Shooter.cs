using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public IShooterHandler ShooterHandler { get => _shooterHandler; set => InitialiseShooterHandler(value); }
    private IShooterHandler _shooterHandler;
    private List<Bullet> _bulletTrackedInstances = new List<Bullet>();

    private BasicTimer FireRateTimer = new BasicTimer();

    public void InitialiseShooterHandler(IShooterHandler handler)
    {
        if (_shooterHandler == null)
        {
            _shooterHandler = handler;
            if (_shooterHandler != null)
            {
                _shooterHandler.OnTryShoot += OnTryShoot;
                FireRateTimer.TargetDuration = _shooterHandler.FireRate;
            }
        }
    }

    private void Start()
    {
        InitialiseShooterHandler(GetComponent<IShooterHandler>());
    }

    private void Update()
    {
        if (FireRateTimer.IsActive)
        {
            FireRateTimer.IsTimerTickOnTarget();
        }
    }

    private void OnTryShoot()
    {
        if (FireRateTimer.IsActive)
        {
            return;
        }

        // Not gonna use ammo for now
        FireRateTimer.StartTimer();
        foreach (BulletSpawnData spawnData in ShooterHandler.BulletsSpawnData)
        {
            if (spawnData.BulletPrefab == null)
            {
                Debug.LogError($"The bullet prefab has not been assigned to {name}, please assign it.");
                continue;
            }

            Bullet instance = Instantiate(spawnData.BulletPrefab, spawnData.Origin, transform.rotation);
            _bulletTrackedInstances.Add(instance);
            instance.Shoot(spawnData.Speed, spawnData.AimDirection, ShooterHandler.IgnoreCollider);
        }
    }
}

public interface IShooterHandler
{
    public delegate void Shoot();
    public event Shoot OnTryShoot;

    public float FireRate { get; }
    public int Ammo { get; }
    public BulletSpawnData[] BulletsSpawnData { get; }
    public Collider IgnoreCollider { get; }
}

public struct BulletSpawnData
{
    public BulletSpawnData(Bullet bulletPrefab, Vector3 aimDirection, Vector3 origin, float speed)
    {
        BulletPrefab = bulletPrefab;
        Origin = origin;
        AimDirection = aimDirection;
        Speed = speed;
    }

    public Bullet BulletPrefab;
    public Vector3 AimDirection;
    public Vector3 Origin;
    public float Speed;
}
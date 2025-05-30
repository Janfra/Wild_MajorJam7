using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public IShooterHandler ShooterHandler { get => _shooterHandler; set => InitialiseShooterHandler(value); }
    private IShooterHandler _shooterHandler;

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

        FireRateTimer.StartTimer();
        // Todo
    }
}

public interface IShooterHandler
{
    public delegate void Shoot();
    public event Shoot OnTryShoot;

    float FireRate { get; }
    int Ammo { get; }
    BulletSpawnData[] BulletsSpawnData { get; }
}

public struct BulletSpawnData
{
    public Vector3 AimDirection;
    public Vector3 Origin;
    float Speed;
}
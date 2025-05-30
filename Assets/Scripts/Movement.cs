using System;
using UnityEngine;

public enum MovementType
{
    FacingDirection,
    World,
}

public class Movement : MonoBehaviour
{
    public IMovementInput MovementInputProvider { get => _movementInputProvider; set => InitialiseMovementInputProvider(value); }
    private IMovementInput _movementInputProvider;

    public void InitialiseMovementInputProvider(IMovementInput provider)
    {
        // Not null checking since it will just keep it as it is
        if (_movementInputProvider == null)
        {
            _movementInputProvider = provider;
        }
        else
        {
            LogInitialiseWarning(typeof(IMovementInput));
        }
    }

    private void Start()
    {
        InitialiseMovementInputProvider(GetComponent<IMovementInput>());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_movementInputProvider != null)
        {
            MoveOnDirection();
        }
    }

    private void MoveOnDirection()
    {
        Rigidbody moveTarget = _movementInputProvider.MoveTarget;
        if (moveTarget == null)
        {
            return;
        }

        Vector2 movementDirection = _movementInputProvider.MovementDirection.normalized;
        Vector3 velocity;
        if (_movementInputProvider.ApplicationType == MovementType.FacingDirection)
        {
            velocity = moveTarget.transform.forward * movementDirection.y + moveTarget.transform.right * movementDirection.x;
        }
        else
        {
            velocity = new Vector3(movementDirection.x, 0.0f, movementDirection.y);
        }

        moveTarget.linearVelocity = velocity * _movementInputProvider.Speed * Time.fixedDeltaTime;
    }

    private void LogInitialiseWarning(Type type)
    {
        Debug.LogWarning($"Movement component should only be initialised when it does not have a valid input provider! - {name} will ignore provided input provider of type {type.Name}");
    }
}

public interface IMovementInput
{
    Vector2 MovementDirection { get; }
    float Speed { get; }
    Rigidbody MoveTarget { get; }
    MovementType ApplicationType { get; }
}

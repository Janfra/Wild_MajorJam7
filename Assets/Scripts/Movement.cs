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
        Vector3 movementDirection = _movementInputProvider.MovementDirection.normalized;
        Vector3 velocity;
        Transform referenceTransform = _movementInputProvider.ReferenceTransform;

        if (_movementInputProvider.ApplicationType == MovementType.FacingDirection)
        {
            velocity = referenceTransform.forward * movementDirection.y + referenceTransform.right * movementDirection.x + movementDirection.z * (-referenceTransform.up);
        }
        else
        {
            velocity = movementDirection;
        }

        _movementInputProvider.SetMovementVelocity(velocity * _movementInputProvider.Speed * Time.fixedDeltaTime);
    }

    private void LogInitialiseWarning(Type type)
    {
        Debug.LogWarning($"Movement component should only be initialised when it does not have a valid input provider! - {name} will ignore provided input provider of type {type.Name}");
    }
}

public interface IMovementInput
{
    Vector3 MovementDirection { get; }
    float Speed { get; }
    MovementType ApplicationType { get; }
    Transform ReferenceTransform { get; }

    public void SetMovementVelocity(Vector3 velocity);
}

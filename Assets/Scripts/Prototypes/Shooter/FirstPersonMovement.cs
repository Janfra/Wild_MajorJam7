using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour, IMovementInput
{
    [SerializeField]
    private PlayerController _player;

    #region Movement Interface
    [Header("Movement")]
    [SerializeField]
    private Rigidbody _rb;
    public Rigidbody Rigidbody => _rb;

    [SerializeField]
    private float _speed = 300.0f;

    [SerializeField]
    private MovementType _movementApplicationType;
    public Vector3 MovementDirection => GetMovementDirection();
    public float Speed => _speed;
    public MovementType ApplicationType => _movementApplicationType;
    public Transform ReferenceTransform => transform;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetMovementVelocity(Vector3 velocity)
    {
        if (_rb)
        {
            _rb.linearVelocity = velocity;
        }
    }

    private Vector3 GetMovementDirection()
    {
        Vector2 input = _player.MovementInput.ReadValue<Vector2>();
        return new Vector3(input.x, 0.0f, input.y);
    }
}

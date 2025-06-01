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
    public Vector2 MovementDirection => _player.MovementInput != null ? _player.MovementInput.ReadValue<Vector2>() : Vector2.zero;
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
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalPlayerMovement : MonoBehaviour, IMovementInput
{
    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private AnimationCurve _speedCurve;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField, Range(0.1f, 5.0f)]
    private float _accelerationMultiplier = 1.0f;

    [SerializeField, Range(0.1f, 5.0f)]
    private float _deccelerationMultiplier = 1.0f;

    private float _speed;

    [SerializeField]
    private MovementType _applicationType;

    public Vector2 MovementDirection => new Vector2(_player.MovementDirection.x, 0.0f);

    public float Speed => _speed;

    public MovementType ApplicationType => _applicationType;

    public Transform ReferenceTransform => transform;

    private float _progress = 0.0f;
    private bool _isAccelerating = false;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        _player.MovementInput.started += StartAccelerating;
        _player.MovementInput.canceled += StartDeccelerating;
    }

    private void Update()
    {
        float curveProgress = 0.0f;
        if (_isAccelerating)
        {
            _progress += Time.deltaTime * _accelerationMultiplier;
        }
        else
        {
            _progress -= Time.deltaTime * _deccelerationMultiplier;
        }

        _progress = Mathf.Clamp01(_progress);
        curveProgress = Mathf.Clamp01(_speedCurve.Evaluate(_progress));
        _speed = Mathf.Lerp(0.0f, _maxSpeed, curveProgress);
    }

    private void StartDeccelerating(InputAction.CallbackContext context)
    {
        _isAccelerating = false;
    }

    private void StartAccelerating(InputAction.CallbackContext context)
    {
        _isAccelerating = true;
    }

    public void SetMovementVelocity(Vector3 velocity)
    {
        _rb.linearVelocity = velocity;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Movement))]
public class HorizontalPlayerMovement : MonoBehaviour, IMovementInput
{
    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private Rigidbody _rb;

    [Header("Animations")]
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AnimatorParameterHasher _velocityParameter;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [Header("Configuration")]
    [SerializeField]
    private AnimationCurve _speedCurve;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField, Range(0.1f, 5.0f)]
    private float _accelerationMultiplier = 1.0f;

    [SerializeField, Range(0.1f, 5.0f)]
    private float _deccelerationMultiplier = 1.0f;

    [SerializeField]
    private Vector2 _Bounds;

    [SerializeField]
    private MovementType _applicationType;

    public Vector3 MovementDirection => new Vector3(_player.MovementInput.ReadValue<Vector2>().x, 0.0f, 0.0f);

    public float Speed => _speed;

    public MovementType ApplicationType => _applicationType;

    public Transform ReferenceTransform => transform;

    private float _speed;
    private float _progress = 0.0f;
    private bool _isAccelerating = false;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Renderer does not update the shader values until masks interactions are changed. Haven't found a proper way to update it, so hacky fix for now.
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
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
        Vector3 futurePosition = transform.position + (velocity * Time.fixedDeltaTime);
        if (futurePosition.x <= _Bounds.x || futurePosition.x >= _Bounds.y)
        {
            _rb.linearVelocity = Vector3.zero;
            _progress *= 0.5f;
            _isAccelerating = false;
            UpdateAnimations(velocity, futurePosition);
            return;
        }

        UpdateAnimations(velocity, futurePosition);
        _rb.linearVelocity = velocity;
    }

    public void UpdateAnimations(Vector3 velocity, Vector3 futurePosition)
    {
        float speed = velocity.sqrMagnitude;
        _animator.SetFloat(_velocityParameter.ID, speed);
        if (!Mathf.Approximately(speed, 0.0f))
        {
            _spriteRenderer.flipX = futurePosition.x > transform.position.x ? false : true;
        }
    }
}

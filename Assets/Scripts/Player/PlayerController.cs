using System;
using System.Xml.XPath;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovementInput
{
    private PlayerInputActions _playerInputs;

    #region Movement Interface
    [Header("Movement")]
    [SerializeField]
    private Rigidbody _rb;
    public Rigidbody Rigidbody => _rb;

    [SerializeField]
    private float _speed = 300.0f;

    [SerializeField]
    private MovementType _movementApplicationType;

    private InputAction _move;
    public Vector2 MovementDirection => _move != null ? _move.ReadValue<Vector2>() : Vector2.zero;
    public float Speed => _speed;
    public MovementType ApplicationType => _movementApplicationType;
    public Transform ReferenceTransform => transform;
    #endregion

    private InputAction _lookAt;
    private InputAction _attack;

    public InputAction MovementInput => _move;
    public InputAction LookInput => _lookAt;
    public InputAction AttackInput => _attack;


    private void Awake()
    {
        _playerInputs = new PlayerInputActions();
        _rb = GetComponent<Rigidbody>();

        _move = _playerInputs.Player.Move;
        _lookAt = _playerInputs.Player.Look;
        _attack = _playerInputs.Player.Attack;
    }

    private void OnEnable()
    {
        _move.Enable();
        _lookAt.Enable();
        _attack.Enable();
    }


    private void OnDisable()
    {
        _move.Disable();
        _lookAt.Disable();
        _attack.Disable();
    }

    public void SetMovementVelocity(Vector3 velocity)
    {
        if (_rb)
        {
            _rb.linearVelocity = velocity;
        }
    }
}

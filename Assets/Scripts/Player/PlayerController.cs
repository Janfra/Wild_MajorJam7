using System;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IMovementInput
{
    private PlayerInputActions _playerInputs;

    #region Movement Interface
    [Header("Movement")]
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _speed = 300.0f;

    private InputAction _move;
    public Vector2 MovementDirection => _move != null ? _move.ReadValue<Vector2>() : Vector2.zero;
    public float Speed => _speed;
    public Rigidbody MoveTarget => _rb;
    public MovementType ApplicationType => MovementType.FacingDirection;
    #endregion

    private InputAction _lookAt;
    public InputAction MovementInput => _move;
    public InputAction LookInput => _lookAt;

    private void Awake()
    {
        _playerInputs = new PlayerInputActions();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _move = _playerInputs.Player.Move;
        _move.Enable();

        _lookAt = _playerInputs.Player.Look;
        _lookAt.Enable();
    }


    private void OnDisable()
    {
        _move.Disable();
        _lookAt.Disable();
    }
}

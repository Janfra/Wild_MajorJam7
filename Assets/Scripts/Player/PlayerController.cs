using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _isEnabledOnStart;

    [SerializeField]
    private GameEvent _enableOnEvent;

    private PlayerInputActions _playerInputs;

    private InputAction _move;
    private InputAction _lookAt;
    private InputAction _attack;
    private InputAction _catchOne;
    private InputAction _catchTwo;
    private InputAction _catchThree;
    private InputAction _catchFour;

    public InputAction MovementInput => _move;
    public InputAction LookInput => _lookAt;
    public InputAction AttackInput => _attack;
    public InputAction CatchOne => _catchOne;
    public InputAction CatchTwo => _catchTwo;
    public InputAction CatchThree => _catchThree;
    public InputAction CatchFour => _catchFour;


    private void Awake()
    {
        _playerInputs = new PlayerInputActions();

        _move = _playerInputs.Player.Move;
        _lookAt = _playerInputs.Player.Look;
        _attack = _playerInputs.Player.Attack;
        _catchOne = _playerInputs.Player.Catch_1;
        _catchTwo = _playerInputs.Player.Catch_2;
        _catchThree = _playerInputs.Player.Catch_3;
        _catchFour = _playerInputs.Player.Catch_4;

        if (_enableOnEvent)
        {
            _enableOnEvent.OnEvent += EnableAllInputs;
        }
    }

    private void Start()
    {
        if (!_isEnabledOnStart)
        {
            DisableAllInputs();
        }
    }

    private void OnEnable()
    {
        EnableAllInputs();
    }


    private void OnDisable()
    {
        DisableAllInputs();
    }

    private void EnableAllInputs()
    {
        _move.Enable();
        _lookAt.Enable();
        _attack.Enable();
        _catchOne.Enable();
        _catchTwo.Enable();
        _catchThree.Enable();
        _catchFour.Enable();
    }

    private void DisableAllInputs()
    {
        _move.Disable();
        _lookAt.Disable();
        _attack.Disable();
        _catchOne.Disable();
        _catchTwo.Disable();
        _catchThree.Disable();
        _catchFour.Disable();
    }
}

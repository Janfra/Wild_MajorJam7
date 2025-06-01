using System;
using System.Xml.XPath;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputs;

    private InputAction _move;
    private InputAction _lookAt;
    private InputAction _attack;
    private InputAction _catchOne;
    private InputAction _catchTwo;
    private InputAction _catchThree;

    public InputAction MovementInput => _move;
    public InputAction LookInput => _lookAt;
    public InputAction AttackInput => _attack;
    public InputAction CatchOne => _catchOne;
    public InputAction CatchTwo => _catchTwo;
    public InputAction CatchThree => _catchThree;


    private void Awake()
    {
        _playerInputs = new PlayerInputActions();

        _move = _playerInputs.Player.Move;
        _lookAt = _playerInputs.Player.Look;
        _attack = _playerInputs.Player.Attack;
        _catchOne = _playerInputs.Player.Catch_1;
        _catchTwo = _playerInputs.Player.Catch_2;
        _catchThree = _playerInputs.Player.Catch_3;
    }

    private void OnEnable()
    {
        _move.Enable();
        _lookAt.Enable();
        _attack.Enable();
        _catchOne.Enable();
        _catchTwo.Enable();
        _catchThree.Enable();
    }


    private void OnDisable()
    {
        _move.Disable();
        _lookAt.Disable();
        _attack.Disable();
        _catchOne.Enable();
        _catchTwo.Enable();
        _catchThree.Enable();
    }
}

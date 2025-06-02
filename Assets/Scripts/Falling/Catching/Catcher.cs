using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CatcherActions
{
    One,
    Two,
    Three,
    Four,
    None,
}

public struct CatchActionState
{
    public CatchActionState(int activationIndex)
    {
        ActivationIndex = activationIndex;
    }

    public bool IsActive => ActivationIndex >= 0;
    public int ActivationIndex;
}

[RequireComponent(typeof(BoxCollider))]
public class Catcher : MonoBehaviour
{
    public delegate void CatchActionUpdate(CatcherActions action);
    public event CatchActionUpdate OnActive;
    public event CatchActionUpdate OnInactive;
    public event CatchActionUpdate OnMain;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    ParticleSystem _catchParticle;
    private ParticleSystem.MainModule _mainModule;

    [SerializeField]
    private CatchActionsUIDataContainer _actionsUIDataContainer;

    public CatcherActions MainAction => _currentMainAction;
    private CatcherActions _currentMainAction = CatcherActions.None;
    private Dictionary<CatcherActions, CatchActionState> _activeActions = new Dictionary<CatcherActions, CatchActionState>();
    private int _activationCount = 0;

    private void Awake()
    {
        if (_player == null)
        {
            throw new System.NullReferenceException($"The Catcher ({name}), has no player controller assigned to it. Please assign it.");
        }

        if (_actionsUIDataContainer)
        {
            _actionsUIDataContainer.SetDataProvider(this);
        }
        else
        {
            Debug.LogError($"The UI data container has not been set. Skipping it on {name}");
        }
    }

    private void Start()
    {
        _player.CatchOne.started += OnCatchActionOne;
        _player.CatchTwo.started += OnCatchActionTwo;
        _player.CatchThree.started += OnCatchActionThree;
        _player.CatchFour.started += OnCatchActionFour;
        _player.CatchOne.canceled += OnCancelCatchActionOne;
        _player.CatchTwo.canceled += OnCancelCatchActionTwo;
        _player.CatchThree.canceled += OnCancelCatchActionThree;
        _player.CatchFour.canceled += OnCancelCatchActionFour;
    }

    public bool IsActionActive(CatcherActions action)
    {
        CatchActionState state;
        if (_activeActions.TryGetValue(action, out state))
        {
            return state.IsActive;
        }
        else
        {
            return false;
        }
    }

    public void SuccessfulCatch()
    {
        _mainModule = _catchParticle.main;
        _mainModule.startColor = Color.green;
        _catchParticle.Play();
    }

    public void MissCatch()
    {
        _mainModule = _catchParticle.main;
        _mainModule.startColor = Color.darkRed;
        _catchParticle.Play();
    }

    private void OnCatchActionOne(InputAction.CallbackContext context)
    {
        SetActionToActive(CatcherActions.One);
    }

    private void OnCatchActionTwo(InputAction.CallbackContext context)
    {
        SetActionToActive(CatcherActions.Two);
    }

    private void OnCatchActionThree(InputAction.CallbackContext context)
    {
        SetActionToActive(CatcherActions.Three);
    }

    private void OnCatchActionFour(InputAction.CallbackContext context)
    {
        SetActionToActive(CatcherActions.Four);
    }

    private void OnCancelCatchActionOne(InputAction.CallbackContext context)
    {
        SetActionToInactive(CatcherActions.One);
    }

    private void OnCancelCatchActionTwo(InputAction.CallbackContext context)
    {
        SetActionToInactive(CatcherActions.Two);
    }

    private void OnCancelCatchActionThree(InputAction.CallbackContext context)
    {
        SetActionToInactive(CatcherActions.Three);
    }

    private void OnCancelCatchActionFour(InputAction.CallbackContext context)
    {
        SetActionToInactive(CatcherActions.Four);
    }

    private void SetActionToActive(CatcherActions action)
    {
        _currentMainAction = action;
        _activationCount++;
        CatchActionState state = new CatchActionState(_activationCount);
        _activeActions[action] = state;
        OnActive?.Invoke(action);
        OnMain?.Invoke(action);
    }

    private void SetActionToInactive(CatcherActions action)
    {
        CatchActionState state = new CatchActionState(-1);
        _activeActions[action] = state;
        OnInactive?.Invoke(action);

        int activationIndex = 0;
        foreach (var pair in _activeActions)
        {
            if (pair.Value.ActivationIndex > activationIndex)
            {
                activationIndex = pair.Value.ActivationIndex;
                _currentMainAction = pair.Key;
            }
        }

        if (activationIndex == 0)
        {
            _currentMainAction = CatcherActions.None;
            _activationCount = 0;
        }
        else
        {
            OnMain?.Invoke(_currentMainAction);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable)
        {
            catchable.OnCatch(this);
        }
    }
}


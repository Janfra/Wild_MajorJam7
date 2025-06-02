using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatchActionsUIDataContainer", menuName = "Scriptable Objects/CatchActionsUIDataContainer")]
public class CatchActionsUIDataContainer : ScriptableObject
{
    [SerializeField]
    private CatchActionUIData[] _targets;

    private Catcher _dataProvider;
    public bool CanProvideInputData => _dataProvider != null;

    private Dictionary<CatcherActions, CatchActionUIData> _actionToUI = new Dictionary<CatcherActions, CatchActionUIData>();
    private CatchActionUIData _currentMain;

    public void SetDataProvider(Catcher dataProvider)
    {
        if (_dataProvider == null)
        {
            _dataProvider = dataProvider;
            InitialiseActionToUIDictionary();
            dataProvider.OnActive += SetTargetAsActive;
            dataProvider.OnInactive += SetTargetAsInactive;
            dataProvider.OnMain += SetTargetAsMain;
        }
    }

    private void InitialiseActionToUIDictionary()
    {
        foreach (CatchActionUIData target in _targets)
        {
            _actionToUI[target.AssociatedAction] = target;
        }
    }

    private void SetTargetAsMain(CatcherActions action)
    {
        if (_actionToUI.TryGetValue(action, out CatchActionUIData data))
        {
            if (_currentMain)
            {
                _currentMain.SetMainState(false);
            }

            _currentMain = data;
            _currentMain.SetMainState(true);
        }
    }

    private void SetTargetAsActive(CatcherActions action)
    {
        SetActionActiveState(action, true);
    }

    private void SetTargetAsInactive(CatcherActions action)
    {
        SetActionActiveState(action, false);
    }

    private void SetActionActiveState(CatcherActions action, bool isActive)
    {
        if (_actionToUI.TryGetValue(action, out CatchActionUIData data))
        {
            data.SetActiveState(isActive);
        }
    }
}

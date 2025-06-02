using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CatchActionUIData", menuName = "Scriptable Objects/CatchActionUIData")]
public class CatchActionUIData : ScriptableObject
{
    public delegate void UIUpdate(bool value);
    public event UIUpdate OnActiveStateChanged;
    public event UIUpdate OnIsMainActionChanged;

    [SerializeField]
    private CatchActionAssociatedData _catchAction;
    public CatchActionAssociatedData CatchActionData => _catchAction;
    public CatcherActions AssociatedAction => _catchAction.Action;

    public void SetActiveState(bool isActive)
    {
        OnActiveStateChanged?.Invoke(isActive);
    }

    public void SetMainState(bool isMain)
    {
        OnIsMainActionChanged?.Invoke(isMain);
    }
}

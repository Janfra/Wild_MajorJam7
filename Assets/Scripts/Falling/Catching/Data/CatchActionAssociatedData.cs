using UnityEngine;

[CreateAssetMenu(fileName = "CatchActionData", menuName = "Scriptable Objects/CatchActionAssociatedData")]
public class CatchActionAssociatedData : ScriptableObject
{
    [SerializeField]
    private Color _colour = Color.white;
    public Color Colour => _colour;

    [SerializeField]
    private CatcherActions _action = CatcherActions.None;
    public CatcherActions Action => _action;
}

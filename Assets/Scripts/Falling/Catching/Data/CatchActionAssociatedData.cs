using UnityEngine;

[CreateAssetMenu(fileName = "CatchActionData", menuName = "Scriptable Objects/CatchActionAssociatedData")]
public class CatchActionAssociatedData : ScriptableObject
{
    [SerializeField]
    private Sprite _fruitSprite;
    public Sprite FruitSprite => _fruitSprite;

    [SerializeField]
    private CatcherActions _action = CatcherActions.None;
    public CatcherActions Action => _action;
}

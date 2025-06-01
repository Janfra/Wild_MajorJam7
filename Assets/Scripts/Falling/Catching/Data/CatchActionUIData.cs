using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CatchActionUIData", menuName = "Scriptable Objects/CatchActionUIData")]
public class CatchActionUIData : ScriptableObject
{
    [SerializeField]
    private CatchActionAssociatedData _catchAction;
    public CatchActionAssociatedData CatchActionData => _catchAction;

    private Catcher _dataProvider;
    public bool CanProvideInputData => _dataProvider != null;
    
    public void SetDataProvider(Catcher dataProvider)
    {
        if (_dataProvider == null)
        {
            _dataProvider = dataProvider;
        }
    }
}

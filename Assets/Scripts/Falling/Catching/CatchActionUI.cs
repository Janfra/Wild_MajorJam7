using UnityEngine;
using UnityEngine.UI;

public class CatchActionUI : MonoBehaviour
{
    [SerializeField]
    private CatchActionUIData _actionData;

    [SerializeField]
    private Image _image;

    private void Start()
    {
        _image.color = _actionData.CatchActionData.Colour;
    }
}

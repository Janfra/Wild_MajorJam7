using System;
using UnityEngine;
using UnityEngine.UI;

public class CatchActionUI : MonoBehaviour
{
    [SerializeField]
    private CatchActionUIData _actionData;

    [SerializeField]
    private Image _colourDisplay;

    [SerializeField]
    private Image _highlightBorder;

    private Color _actionColor => _actionData.CatchActionData.Colour;
    private bool _isActive;

    private void Start()
    {
        _colourDisplay.color = _actionColor;
        _highlightBorder.color = _actionColor;

        _actionData.OnActiveStateChanged += UpdateHighlightBorder;
        _actionData.OnIsMainActionChanged += UpdateUIMainAction;
    }

    private void UpdateUIMainAction(bool isMain)
    {
        if (isMain)
        {
            HighlightToMain();
        }
        else
        {
            if (_isActive)
            {
                HighlightToActive();
            }
            else
            {
                UndoHighlight();
            }
        }
    }

    private void UpdateHighlightBorder(bool isActive)
    {
        _isActive = isActive;
        if (isActive)
        {
            HighlightToActive();
        }
        else
        {
            UndoHighlight();
        }
    }

    private void HighlightToMain()
    {
        _highlightBorder.color = Color.white;
    }

    private void HighlightToActive()
    {
        _highlightBorder.color = Color.black;
    }

    private void UndoHighlight()
    {
        _highlightBorder.color = _actionColor;
    }
}

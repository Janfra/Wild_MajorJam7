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
        if (_colourDisplay)
        {
            _colourDisplay.color = _actionColor;
        }

        if (_highlightBorder)
        {
            _highlightBorder.color = _actionColor;
        }

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
        if (_highlightBorder)
        {
            _highlightBorder.color = Color.white;
        }
    }

    private void HighlightToActive()
    {
        if (_highlightBorder)
        {
            _highlightBorder.color = Color.black;
        }
    }

    private void UndoHighlight()
    {
        if (_highlightBorder)
        {
            _highlightBorder.color = _actionColor;
        }
    }
}

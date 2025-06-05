using System;
using UnityEngine;
using UnityEngine.UI;

public class CatchActionUI : MonoBehaviour
{
    [SerializeField]
    private CatchActionUIData _actionData;

    [SerializeField]
    private Image _highlight;

    [SerializeField]
    private HighlightSettings _highlightColours;

    private bool _isActive;

    private void Start()
    {
        if (_highlight)
        {
            _highlight.color = Color.white;
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
                HighlightToInactive();
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
            HighlightToInactive();
        }
    }

    private void HighlightToMain()
    {
        if (_highlight)
        {
            _highlight.color = _highlightColours.Selected;
        }
    }

    private void HighlightToActive()
    {
        if (_highlight)
        {
            _highlight.color = _highlightColours.Active;
        }
    }

    private void HighlightToInactive()
    {
        if (_highlight)
        {
            _highlight.color = _highlightColours.Inactive;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class CatchActionUI : MonoBehaviour
{
    [SerializeField]
    private CatchActionUIData _actionData;

    [SerializeField]
    private Image _highlight;

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
        if (_highlight)
        {
            _highlight.color = Color.white;
        }
    }

    private void HighlightToActive()
    {
        if (_highlight)
        {
            _highlight.color = Color.black;
        }
    }

    private void UndoHighlight()
    {
        if (_highlight)
        {
            _highlight.color = Color.whiteSmoke;
        }
    }
}

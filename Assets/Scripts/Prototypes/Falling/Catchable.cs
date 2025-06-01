using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Catchable : MonoBehaviour
{
    public delegate void CatchableAction();
    public event CatchableAction OnCatched;
    public event CatchableAction OnMissed;
    public event CatchableAction OnFailed;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private ColorSwap _colourLerper;
    private Coroutine _colorLerpCoroutine;
    private IEnumerator _lerpMethod;


    private void Awake()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }

    public virtual void OnCatch()
    {
        Debug.Log($"Catched");
        OnCatched?.Invoke();
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public virtual void OnMiss()
    {
        OnMissed?.Invoke();
    }

    public virtual void OnFail()
    {
        OnFailed?.Invoke();
    }

    public void OnFocus()
    {
        Debug.Log($"Focusing catchable");
        // not very readable, but for simplicity gonna leave it for now
        _colourLerper.StartTransition(true);
        StartColourTransition();
    }

    public void OnUnfocus()
    {
        Debug.Log($"Unfocusing catchable");
        _colourLerper.StartTransition(false);
        StartColourTransition();
    }

    private void StartColourTransition()
    {
        if (_colorLerpCoroutine == null)
        {
            _lerpMethod = ChangeColour();
            _colorLerpCoroutine = StartCoroutine(_lerpMethod);
        }
    }

    private IEnumerator ChangeColour()
    {
        while (!_colourLerper.IsCompleted())
        {
            _spriteRenderer.color = _colourLerper.GetColour(Time.deltaTime);
            yield return null;
        }

        _spriteRenderer.color = _colourLerper.GetColour(Time.deltaTime);
        _colorLerpCoroutine = null;
    }

}

[Serializable]
public class ColorSwap
{
    [SerializeField]
    private Color _defaultColour;
    [SerializeField]
    private Color _focusColour;
    [SerializeField]
    private float _transitionDuration;
    private float _progress;
    private bool _isFocusTransition;

    private float _alpha => _progress / _transitionDuration;

    public void StartTransition(bool isFocusTransition)
    {
        _isFocusTransition = isFocusTransition;
        _progress += _isFocusTransition ? 0.01f : -0.01f;
    }

    public Color GetColour(float deltaTime)
    {
        if (_isFocusTransition)
        {
            _progress = GetClampedProgress(_progress + deltaTime);
        }
        else
        {
            _progress = GetClampedProgress(_progress - deltaTime);
        }

        return Color.Lerp(_defaultColour, _focusColour, _alpha);
    }

    public bool IsCompleted()
    {
        float alpha = _alpha;
        return alpha <= 0.0f || alpha >= 1.0f;
    }

    private float GetClampedProgress(float progress)
    {
        return Mathf.Clamp(progress, 0.0f, _transitionDuration);
    }
}
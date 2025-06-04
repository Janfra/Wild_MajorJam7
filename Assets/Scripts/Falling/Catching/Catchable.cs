using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Catchable : MonoBehaviour
{
    public delegate void CatchableAction();
    public event CatchableAction OnCatched;
    public event CatchableAction OnMissed;
    public event CatchableAction OnFailed;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private CatchActionAssociatedData _requiredAction;

    [SerializeField]
    private ColorSwap _colourLerper;

    private const int _FailLayer = 8;

    private Coroutine _colorLerpCoroutine;
    private IEnumerator _lerpMethod;
    private float _bounceModifier = 0.0f;
    private ScreenMarkerTrackerHandle _offscreenTrackerHandle;

    private void Awake()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        _colourLerper.SetFocusColour(_requiredAction);
    }

    private void Start()
    {
        _offscreenTrackerHandle = OffscreenMarkers.Instance.TrackWithMarkerWhileOffscreen(transform);
    }

    public void OnDrop(CatchActionAssociatedData requiredAction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        if (requiredAction != null)
        {
            _requiredAction = requiredAction;
            _colourLerper.SetFocusColour(_requiredAction);
        }
    }

    public virtual void OnCatch(Catcher catcher)
    {
        if (catcher.MainAction == _requiredAction.Action)
        {
            Debug.Log("catched");
            OnCatched?.Invoke();
            catcher.SuccessfulCatch();
        }
        else
        {
            OnMiss();
            catcher.MissCatch();
        }

        ClearFruit();
    }

    public virtual void OnMiss()
    {
        Debug.Log("Missed");
        OnMissed?.Invoke();
    }

    public virtual void OnFail()
    {
        gameObject.layer = _FailLayer;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(((Vector3.forward * (1.3f - _bounceModifier)) + Vector3.up).normalized * 6.0f, ForceMode.Impulse);
        _bounceModifier = 0.2f;

        OnFailed?.Invoke();
        StartCoroutine(ClearFruitAfterTimer());
    }

    protected void ClearEventSubscribers()
    {
        OnCatched = null;
        OnMissed = null;
        OnFailed = null;
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

    private void ClearFruit()
    {
        ClearEventSubscribers();
        StopAllCoroutines();
        Destroy(gameObject);
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

    private IEnumerator ClearFruitAfterTimer()
    {
        yield return new WaitForSeconds(3.0f);
        ClearFruit();
    }

    private void OnDestroy()
    {
        if (_offscreenTrackerHandle.IsValid)
        {
            OffscreenMarkers.Instance.ReleaseScreenTracker(_offscreenTrackerHandle);
        }
    }

}

[Serializable]
public class ColorSwap
{
    [SerializeField]
    private Color _defaultColour;
    private Color _focusColour;
    [SerializeField]
    private float _transitionDuration;
    private float _progress;
    private bool _isFocusTransition;

    private float _alpha => _progress / _transitionDuration;

    public void SetFocusColour(CatchActionAssociatedData data)
    {
        _focusColour = data.Colour;
    }

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
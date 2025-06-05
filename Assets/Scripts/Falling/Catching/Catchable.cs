using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(FallMovement))]
public class Catchable : MonoBehaviour
{
    public delegate void CatchableAction();
    public event CatchableAction OnCatched;
    public event CatchableAction OnMissed;
    public event CatchableAction OnFailed;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Rigidbody _rb;


    [SerializeField]
    private ColorSwap _colourLerper;

    private const int _FailLayer = 8;

    private CatcherActions _requiredAction;
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

        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        } 

        _spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }

    private void Start()
    {
        _offscreenTrackerHandle = OffscreenMarkers.Instance.TrackWithMarkerWhileOffscreen(transform);
    }

    public void OnDrop(CatchActionAssociatedData requiredAction)
    {
        if (requiredAction != null)
        {
            _requiredAction = requiredAction.Action;
            _spriteRenderer.sprite = requiredAction.FruitSprite;

            _colourLerper.SetFocusColour(requiredAction);
        }
    }

    public virtual void OnCatch(Catcher catcher)
    {
        if (catcher.MainAction == _requiredAction)
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
        Movement movement = GetComponent<Movement>();
        movement.enabled = false;
        _rb.useGravity = true;

        gameObject.layer = _FailLayer;
        OnFailed?.Invoke();
        StartCoroutine(ClearFruitAfterTimer());
    }

    public void OnBounce()
    {
        _rb.AddForce(((Vector3.forward * (1.3f - _bounceModifier)) + Vector3.up).normalized * 6.0f, ForceMode.Impulse);
        _bounceModifier = 0.2f;
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
        //_colourLerper.StartTransition(true);
        //StartColourTransition();
    }

    public void OnUnfocus()
    {
        Debug.Log($"Unfocusing catchable");
        //_colourLerper.StartTransition(false);
        //StartColourTransition();
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
        // Removing colour during transition to new action system
        _focusColour = Color.white;
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
using System;
using UnityEngine;

[Serializable]
public class BasicTimer
{
    public delegate void TimerCallback();
    protected event TimerCallback onTimerCallback;

    public bool IsActive => _isActive;
    private bool _isActive;

    public float TargetDuration
    {
        get { return _targetDuration; }
        set { _targetDuration = Mathf.Clamp(value, 0.0f, Mathf.Infinity); }
    }

    public float ProgressValue
    {
        get { return Mathf.Clamp01(currentTime / _targetDuration); }
    }

    [SerializeField]
    private float _targetDuration;
    protected float currentTime = 0.0f;

    public void StartTimer()
    {
        _isActive = true;
    }

    public void StopTimer()
    {
        _isActive = false;
        ResetProgress();
    }

    // Ticks the state timer until it reaches the target duration.
    // Returns: Has the timer duration been reached
    public virtual bool IsTimerTickOnTarget(bool consumeCallbacks = true)
    {
        currentTime += Time.deltaTime;
        bool targetReached = currentTime >= _targetDuration;
        if (targetReached)
        {
            currentTime = 0.0f;
            _isActive = false;

            if (consumeCallbacks)
            {
                ConsumeTimerCallback();
            }
            else
            {
                onTimerCallback?.Invoke();
            }
        }
        return targetReached;
    }

    public void ResetProgress()
    {
        currentTime = 0.0f;
    }

    public void SubscribeToCallback(TimerCallback callback)
    {
        onTimerCallback += callback;
    }

    public void UnsubscribeToCallback(TimerCallback callback)
    {
        onTimerCallback -= callback;
    }

    public void ClearCallback()
    {
        onTimerCallback = null;
    }
    protected void ConsumeTimerCallback()
    {
        onTimerCallback?.Invoke();
        ClearCallback();
    }
}
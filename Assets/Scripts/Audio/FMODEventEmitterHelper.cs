using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class FMODEventEmitterHelper : MonoBehaviour
{
    [SerializeField]
    private FMODEvent _audioEvent;

    [SerializeField]
    private StudioEventEmitter _emitter;
    private ISoundEmitter _emitterNotifier;

    private void OnValidate()
    {
        if (!_emitter)
        {
            _emitter = GetComponent<StudioEventEmitter>();
            if (_audioEvent)
            {
                _emitter.EventReference = _audioEvent.Event;
            }
        }
    }

    private void Awake()
    {
        if (!_emitter) 
        {
            _emitter = GetComponent<StudioEventEmitter>();
        }

        if (_audioEvent)
        {
            _emitter.EventReference = _audioEvent.Event;
        }

        _emitterNotifier = GetComponent<ISoundEmitter>();
        if (_emitterNotifier != null) 
        {
            _emitterNotifier.OnStartEmitting += StartPlaying;
            _emitterNotifier.OnStopEmitting += StartPlaying;
        }
    }

    public void StartPlaying()
    {
        _emitter.Play();
    }

    public void StopPlaying()
    {
        _emitter.Stop();
    }

    private void OnDestroy()
    {
        StopPlaying();
    }
}

public interface ISoundEmitter
{
    public delegate void EmitAction();
    public event EmitAction OnStartEmitting;
    public event EmitAction OnStopEmitting;
}

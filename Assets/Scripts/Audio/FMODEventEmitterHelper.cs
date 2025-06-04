using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class FMODEventEmitterHelper : MonoBehaviour
{
    [SerializeField]
    private FMODEvent _audioEvent;

    [SerializeField]
    private StudioEventEmitter _emitter;

    [SerializeField]
    private GameEvent _startEvent;

    [SerializeField]
    private GameEvent _endEvent;

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

        if (_startEvent) 
        {
            _startEvent.OnEvent += StartPlaying;
        }

        if (_endEvent)
        {
            _endEvent.OnEvent += StopPlaying;
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

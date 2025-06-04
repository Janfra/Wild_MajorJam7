using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "FMODEventInstance", menuName = "Scriptable Objects/FMODEventInstance")]
public class FMODEventInstance : ScriptableObject
{
    [SerializeField]
    private FMODEvent _audioEvent;
    private EventInstance _audioEventInstance;

    public void CreateAudioInstance()
    {
        if (!_audioEvent.IsValid)
        {
            return;
        }

        if (_audioEventInstance.isValid())
        {
            return;
        }

        EventInstance audioInstance = RuntimeManager.CreateInstance(_audioEvent.Event);
        _audioEventInstance = audioInstance;
    }

    public void ReleaseAudioInstance()
    {
        if (!_audioEventInstance.isValid())
        {
            return;
        }

        _audioEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _audioEventInstance.release();
    }

    public void StartLoopingAudio()
    {
        if (!_audioEventInstance.isValid())
        {
            Debug.LogWarning($"Cant start looping audio, event instance was not created for {name}");
            return;
        }

        PLAYBACK_STATE playbackState;
        _audioEventInstance.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            _audioEventInstance.start();
        }
    }

    public void StopLoopingAudio(bool isImmediate)
    {
        if (!_audioEventInstance.isValid())
        {
            return;
        }

        FMOD.Studio.STOP_MODE stopMode = isImmediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT;
        _audioEventInstance.stop(stopMode);
    }

    private void OnDisable()
    {
        ReleaseAudioInstance();
    }

    private void OnDestroy()
    {
        ReleaseAudioInstance();
    }
}

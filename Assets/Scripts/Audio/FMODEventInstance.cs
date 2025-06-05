using FMOD;
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
            UnityEngine.Debug.LogWarning($"Cant start looping audio, event instance was not created for {name}");
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

    public bool TryGetParameterID(string parameterName, out PARAMETER_ID parameterID)
    {
        if (!_audioEventInstance.isValid())
        {
            parameterID = default;
            return false;
        }

        // Could instead cache them all at the beginning, but exposing them would be extra work, so for now will do
        RESULT result = _audioEvent.EventDescription.getParameterDescriptionByName(parameterName, out PARAMETER_DESCRIPTION parameterDescription);
        if (result.Equals(RESULT.OK))
        {
            parameterID = parameterDescription.id;
            return true;
        }

        UnityEngine.Debug.LogWarning($"Failed to receive parameter description {parameterName}, result: {result}");
        parameterID = default;
        return false;
    }

    public void SetGlobalParameter(PARAMETER_ID parameterID, float value)
    {
        if (!_audioEventInstance.isValid())
        {
            return;
        }

        RuntimeManager.StudioSystem.setParameterByID(parameterID, value);
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

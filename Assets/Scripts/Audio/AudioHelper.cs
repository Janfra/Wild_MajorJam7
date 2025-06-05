using System;
using FMOD.Studio;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private FMODEventInstance _music;

    [SerializeField]
    private string _startMusicParameterName;

    [SerializeField]
    private GameEvent _gameStarted;

    private void Awake()
    {
        if (_gameStarted)
        {
            _gameStarted.OnEvent += StartMainSong;
        }
    }

    private void StartMainSong()
    {
        if (_music.TryGetParameterID(_startMusicParameterName, out PARAMETER_ID parameterID))
        {
            _music.SetGlobalParameter(parameterID, 1.0f);
        }
        else
        {
            throw new Exception($"Parameter name provided {_startMusicParameterName} was not valid. Verify the parameter name");
        }
    }

    private void Start()
    {
        _music.CreateAudioInstance();
        if (_music)
        {
            _music.StartLoopingAudio();
        }
    }
}

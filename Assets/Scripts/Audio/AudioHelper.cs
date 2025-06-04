using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private FMODEventInstance _music;

    private void Awake()
    {
        _music.CreateAudioInstance();
    }

    private void Start()
    {
        if (_music)
        {
            _music.StartLoopingAudio();
        }
    }
}

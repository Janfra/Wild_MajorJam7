using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private FMODEventInstance _music;

    private void Start()
    {
        _music.CreateAudioInstance();
        if (_music)
        {
            _music.StartLoopingAudio();
        }
    }
}

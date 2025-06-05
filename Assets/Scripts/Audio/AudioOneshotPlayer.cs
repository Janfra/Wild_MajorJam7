using UnityEngine;

public class AudioOneshotPlayer : MonoBehaviour
{
    [SerializeField]
    private FMODEvent _audio;

    [SerializeField]
    private Transform _target;

    public void PlayOneshot()
    {
        if (_target)
        {
            _audio.PlayOneshot(_target.position);
        }
        else
        {
            _audio.PlayOneshot();
        }
    }
}

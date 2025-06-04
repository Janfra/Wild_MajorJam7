using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "FMODEvent", menuName = "Scriptable Objects/FMODEvent")]
public class FMODEvent : ScriptableObject
{
    [SerializeField]
    private EventReference _audioEvent;
    public EventReference Event => _audioEvent;

    public bool IsValid => !_audioEvent.IsNull;
    
    public void PlayOneshot(Vector3 position)
    {
        if (!IsEventUsable())
        {
            return;
        }

        RuntimeManager.PlayOneShot(_audioEvent, position);
    }

    private bool IsEventUsable()
    {
        if (_audioEvent.IsNull)
        {
            Debug.LogWarning($"Trying to play null audio event from FMODEvent {name}");
            return false;
        }

        return true;
    }
}

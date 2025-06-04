using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "FMODBus", menuName = "Scriptable Objects/FMODBus")]
public class FMODBus : ScriptableObject
{
    [SerializeField]
    private string _busName;
    private Bus _bus;
    public Bus Bus => _bus.isValid() ? _bus : _bus = GetBus();

    public void SetBusVolume(float volume)
    {
        Bus.setVolume(Mathf.Clamp01(volume));
    }

    private Bus GetBus()
    {
        return RuntimeManager.GetBus(GetBusName());
    }

    private string GetBusName()
    {
        return $"bus:/{_busName}";
    }
}

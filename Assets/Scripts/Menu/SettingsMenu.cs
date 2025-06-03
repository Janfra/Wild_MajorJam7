using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _audioVolume;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AnimationClipHasher _menuAppearAnimation;

    [SerializeField]
    private AnimationClipHasher _menuCloseAnimation;

    [SerializeField]
    private GameEvent _settingsClosed;

    [SerializeField]
    private GameEvent _settingsOpened;

    private const string _masterVolumeName = "MasterVolume";

    private void Awake()
    {
        _settingsOpened.OnEvent += OnShowMenu;
    }

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(_masterVolumeName, 0.75f); ;
        _audioVolume.value = volume;
        _audioMixer.SetFloat(_masterVolumeName, GetSliderValueAsVolume(volume));
    }

    public void UpdateGameVolume(float volume)
    {
        if (!_audioMixer)
        {
            return;
        }

        _audioMixer.SetFloat(_masterVolumeName, GetSliderValueAsVolume(volume));
        PlayerPrefs.SetFloat(_masterVolumeName, volume);
    }

    public void OnShowMenu()
    {
        _animator.Play(_menuAppearAnimation.AnimationHash);
    }

    public void OnCloseMenu()
    {
        _settingsClosed?.InvokeEvent();
        _animator.Play(_menuCloseAnimation.AnimationHash);
    }

    private float GetSliderValueAsVolume(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}

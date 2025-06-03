using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameEvent _gameStarted;

    [SerializeField]
    private AnimationClipHasher _menuCloseAnimation;

    [SerializeField]
    private GameEvent _showSettings;

    [SerializeField]
    private GameObject[] _logicObjects;

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public void StartGame()
    {
        foreach (var item in _logicObjects)
        {
            item.SetActive(false);
        }

        _animator.Play(_menuCloseAnimation.AnimationHash);
        if (_gameStarted == null)
        {
            throw new System.NullReferenceException($"Please assign the game started event to {name} object to notify objects of game started");
        }

        _gameStarted.InvokeEvent();
    }

    public void ShowSettings()
    {
        _showSettings?.InvokeEvent();
    }
}

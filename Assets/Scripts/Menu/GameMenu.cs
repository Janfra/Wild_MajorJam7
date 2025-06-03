using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameEvent _gameStarted;

    [SerializeField]
    private AnimationClipHasher _menuAppearAnimation;

    private void Awake()
    {
        if (_gameStarted)
        {
            _gameStarted.OnEvent += OnGameStart;
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void OnGameStart()
    {
        _animator.Play(_menuAppearAnimation.AnimationHash);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "AnimationClipHasher", menuName = "Scriptable Objects/AnimationClipHasher")]
public class AnimationClipHasher : ScriptableObject
{
    [SerializeField]
    private AnimationClip _animationClip;

    public int AnimationHash => _hash;
    private int _hash;
    private bool _isInitialised = false;

    private void OnEnable()
    {
        Initialise();
    }

    public void Initialise(bool isForced = false)
    {
        if (!_isInitialised || isForced)
        {
            _hash = Animator.StringToHash(_animationClip.name);
            _isInitialised = true;
        }
    }
}

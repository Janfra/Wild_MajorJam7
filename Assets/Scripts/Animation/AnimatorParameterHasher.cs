using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorParameterHasher", menuName = "Scriptable Objects/AnimatorParameterHasher")]
public class AnimatorParameterHasher : ScriptableObject
{
    [SerializeField]
    private string _parameterName;
    public int ID => _id;
    private int _id;
    private bool _isInitialised = false;

    private void OnEnable()
    {
        Initialise();
    }

    public void Initialise(bool isForced = false)
    {
        if ((!_isInitialised || isForced) && _parameterName.Length > 0)
        {
            _id = Animator.StringToHash(_parameterName);
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "MarkerSettings", menuName = "Scriptable Objects/MarkerSettings")]
public class MarkerSettings : ScriptableObject
{
    [SerializeField]
    private Vector2 _positionOffset;
    public Vector2 PositionOffset => _positionOffset;
}

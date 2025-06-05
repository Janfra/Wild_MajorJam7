using UnityEngine;

[CreateAssetMenu(fileName = "HighlightSettings", menuName = "Scriptable Objects/HighlightSettings")]
public class HighlightSettings : ScriptableObject
{
    [SerializeField]
    private Color _selected;
    public Color Selected => _selected;

    [SerializeField]
    private Color _active;
    public Color Active => _active;

    [SerializeField]
    private Color _inactive;
    public Color Inactive => _inactive;
}

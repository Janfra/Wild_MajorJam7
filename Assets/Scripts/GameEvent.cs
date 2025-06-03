using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public delegate void Event();
    public event Event OnEvent;

    public void InvokeEvent()
    {
        OnEvent?.Invoke();
    }
}

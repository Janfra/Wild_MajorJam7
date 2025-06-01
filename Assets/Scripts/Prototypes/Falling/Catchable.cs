using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Catchable : MonoBehaviour
{
    public delegate void Catched();
    public event Catched OnCatched;

    public virtual void OnCatch()
    {
        Debug.Log($"Catched");
        OnCatched?.Invoke();
    }

    public void OnFocus()
    {
        Debug.Log($"Focusing catchable");
    }

    public void OnUnfocus()
    {
        Debug.Log($"Unfocusing catchable");
    }
}

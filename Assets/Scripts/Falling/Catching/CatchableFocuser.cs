using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CatchableFocuser : MonoBehaviour
{
    private Catchable _focused;

    private void OnTriggerEnter(Collider collision)
    {
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable && catchable != _focused)
        {
            if (_focused == null)
            {
                SetAsFocused(catchable);
            }
            else if (catchable.transform.position.y < _focused.transform.position.y)
            {
                UnsetAsFocused();
                SetAsFocused(catchable);
            }
        }
    }

    private void UnsetAsFocused()
    {
        _focused.OnUnfocus();
        _focused.OnCatched -= ClearFocused;
        _focused.OnMissed -= ClearFocused;
        _focused.OnFailed -= ClearFocused;
    }

    private void SetAsFocused(Catchable catchable)
    {
        _focused = catchable;
        _focused.OnFocus();
        _focused.OnCatched += ClearFocused;
        _focused.OnMissed += ClearFocused;
        _focused.OnFailed += ClearFocused;
    }

    private void ClearFocused()
    {
        UnsetAsFocused();
        _focused = null;
    }
}

using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FailLine : MonoBehaviour
{
    [SerializeField]
    private GameEvent _fruitMissedEvent;

    private void OnCollisionEnter(Collision collision)
    {
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable)
        {
            catchable.OnFail();
            _fruitMissedEvent?.InvokeEvent();
        }
    }
}

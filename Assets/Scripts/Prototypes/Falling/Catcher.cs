using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Catcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable)
        {
            catchable.OnCatch();
        }
    }
}


using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FailLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable)
        {
            catchable.OnFail();
        }
    }
}

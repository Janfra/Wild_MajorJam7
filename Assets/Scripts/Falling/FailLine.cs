using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FailLine : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable)
        {
            // Should create a bounce specific one, but for now it'll double as fail and bounce
            catchable.OnFail();
        }
    }
}

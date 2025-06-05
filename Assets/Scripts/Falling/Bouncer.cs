using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bouncer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Could be an interface, not needed in this scenario
        Catchable catchable = collision.gameObject.GetComponent<Catchable>();
        if (catchable)
        {
            catchable.OnBounce();
        }
    }
}

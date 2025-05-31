using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected Collider bulletCollider;

    protected Rigidbody rb;
    protected float travelSpeed;
    protected Vector3 travelDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Shoot(float speed, Vector3 aimDirection, Collider ownerCollider)
    {
        travelSpeed = speed;
        travelDirection = aimDirection.normalized;
        rb.linearVelocity = travelDirection * travelSpeed * Time.fixedDeltaTime;
        Physics.IgnoreCollision(bulletCollider, ownerCollider, true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
    
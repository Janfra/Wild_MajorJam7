using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Movement))]
public class FallMovement : MonoBehaviour, IMovementInput
{
    [SerializeField]
    private float _fallSpeed = 10.0f;

    [SerializeField]
    private Rigidbody _rb;

    #region Movement Input Interface
    public Vector3 MovementDirection => Vector3.down;

    public float Speed => _fallSpeed;

    public MovementType ApplicationType => MovementType.World;

    public Transform ReferenceTransform => transform;

    public void SetMovementVelocity(Vector3 velocity)
    {
        _rb.linearVelocity = velocity;
    }
    #endregion

    public void SetFallSpeed(float speed)
    {
        _fallSpeed = Mathf.Clamp(speed, 10.0f, Mathf.Infinity);
    }

    private void Awake()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }
}

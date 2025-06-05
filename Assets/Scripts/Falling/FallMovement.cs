using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Movement))]
public class FallMovement : MonoBehaviour, IMovementInput
{
    [SerializeField]
    private float _fallSpeed = 3.0f;

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

    private void Awake()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }
}

using UnityEngine;

public class FirstPersonPlayerCamera : PlayerCamera
{
    [SerializeField]
    private FirstPersonMovement _playerMovement;

    [Header("Rotation")]
    [SerializeField]
    private Vector2 _mouseSensitivity = Vector2.one;
    [SerializeField]
    private Vector2 _verticalClamp;

    private Vector2 _rotation = Vector2.zero;

    protected override void OnCameraUpdate()
    {
        base.OnCameraUpdate();

        Vector2 input = player.LookInput.ReadValue<Vector2>();
        _rotation.x -= (input.y * Time.fixedDeltaTime) * _mouseSensitivity.x;
        _rotation.x = Mathf.Clamp(_rotation.x, _verticalClamp.x, _verticalClamp.y);
        _rotation.y += (input.x * Time.fixedDeltaTime) * _mouseSensitivity.y;
        transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0.0f);
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerMovement.Rigidbody.MoveRotation(Quaternion.Euler(0.0f, _rotation.y, 0.0f));
    }
}

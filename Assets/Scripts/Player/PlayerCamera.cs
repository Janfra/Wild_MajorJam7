using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    [Header("Rotation")]
    [SerializeField]
    private Vector2 _mouseSensitivity = Vector2.one;
    [SerializeField]
    private Vector2 _verticalClamp;

    private Vector2 _rotation = Vector2.zero;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position;

        Vector2 input = _player.LookInput.ReadValue<Vector2>();
        _rotation.x -= (input.y * Time.fixedDeltaTime) * _mouseSensitivity.x;
        _rotation.x = Mathf.Clamp(_rotation.x, _verticalClamp.x, _verticalClamp.y);
        _rotation.y += (input.x * Time.fixedDeltaTime) * _mouseSensitivity.y;
        transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _player.MoveTarget.MoveRotation(Quaternion.Euler(0.0f, _rotation.y, 0.0f));
    }
}

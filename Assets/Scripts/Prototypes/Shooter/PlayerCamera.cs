using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    protected PlayerController player;

    [SerializeField]
    protected Vector3 positionOffset;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        OnCameraUpdate();
    }

    protected virtual void OnCameraUpdate()
    {
        transform.position = player.transform.position + positionOffset;
    }
}

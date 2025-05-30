using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        Camera playerCamera = Camera.main;
        transform.LookAt(playerCamera.transform.position);
    }
}

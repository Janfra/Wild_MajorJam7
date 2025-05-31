using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        Camera playerCamera = Camera.main;
        transform.LookAt(playerCamera.transform.position);
    }
}

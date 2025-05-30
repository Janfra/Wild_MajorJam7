using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Camera playerCamera = Camera.main;
        transform.LookAt(playerCamera.transform.position);
    }
}

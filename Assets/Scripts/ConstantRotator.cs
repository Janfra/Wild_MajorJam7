using UnityEngine;

public class ConstantRotator : MonoBehaviour
{
    [SerializeField]
    private float _rotationRate;

    private void Update()
    {
        transform.Rotate(0.0f, 0.0f, _rotationRate * Time.deltaTime);
    }
}

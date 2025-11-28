using UnityEngine;

public class CollectibleObject3D : MonoBehaviour
{
    public float rotationSpeed;

    private void FixedUpdate()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);
    }
}

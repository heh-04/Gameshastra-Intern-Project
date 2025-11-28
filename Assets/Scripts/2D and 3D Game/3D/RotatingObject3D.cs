using UnityEngine;

public class RotatingObject3D : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private void Start()
    {
        RandomAttributes();
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void RandomAttributes()
    {
        int[] directions = { -1, 1 };
        transform.localScale = new Vector3(Random.Range(0.8f, 2f), 1f, 1f);
        rotationSpeed = Random.Range(70f, 120f) * directions[Random.Range(0, directions.Length)];
    }
}

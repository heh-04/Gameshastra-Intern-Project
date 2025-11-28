using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private bool isRandomized = false;
    private Vector3 randomOffset;
    private Vector3 pointA;
    private Vector3 pointB;
    public Vector3 offset;
    public float moveSpeed = 1f;


    private void Start()
    {
        if (isRandomized)
        {
            RandomAttributes();
        }

        else
        {
            pointA = transform.position + offset;
            pointB = transform.position - offset;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * moveSpeed, 1));
    }

    private void RandomAttributes()
    {
        int[] angles = { 0, 90, 180, 270 };
        transform.localRotation = Quaternion.Euler(0, angles[Random.Range(0, angles.Length - 1)], 0);
        transform.localScale = new Vector3(Random.Range(0.5f, 2f), 0.2f, 0.2f);

        if (transform.localRotation.y == 0 || transform.localRotation.y == 180)
        {
            randomOffset = new Vector3(Random.Range(transform.localScale.x, transform.localScale.x * 2), 0, 0);
        }
        else
        {
            randomOffset = new Vector3(0, 0, Random.Range(transform.localScale.x, transform.localScale.x * 2));
        }

        moveSpeed = Random.Range(0.5f, 2f);

        pointA = transform.position + randomOffset;
        pointB = transform.position - randomOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(transform, true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(null, true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(transform, true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(null, true);
            }
        }
    }
}

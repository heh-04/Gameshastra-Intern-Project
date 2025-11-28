using UnityEngine;

public class Camera2D : MonoBehaviour
{
    public Vector2 offset;

    private float playerPositionX;
    private Vector3 cameraPosition;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        playerPositionX = player.transform.position.x;
        cameraPosition.x = playerPositionX + offset.x;
        cameraPosition.y = offset.y;
        cameraPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, 0.5f);
    }
}

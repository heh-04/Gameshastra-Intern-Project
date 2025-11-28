using UnityEngine;
using UnityEngine.InputSystem;

public class Camera3D : MonoBehaviour
{
    public float cameraLookAtOffset = 1f;
    public Vector3 cameraOffset;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        Vector3 cameraPosition = player.transform.position + player.transform.forward * cameraOffset.z + player.transform.up * cameraOffset.y;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, 0.5f);
        Vector3 lookDirection = (new Vector3(player.transform.position.x, (player.transform.position.y + cameraLookAtOffset), player.transform.position.z) - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), 1f);
    }
}



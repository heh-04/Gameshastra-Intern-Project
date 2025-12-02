using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollisions3D : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerEvents.Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            ObjectSpawner3D spawner = FindAnyObjectByType<ObjectSpawner3D>();
            other.transform.position = spawner.GetRandomPointInBounds(spawner.targetCollider.bounds, spawner.distanceBetweenCollectibles, spawner.collectibleLayer, spawner.transform.localScale.y + 0.5f);
            PlayerScore.instance.AddScore(1);
        }
    }
}

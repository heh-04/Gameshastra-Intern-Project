using UnityEngine;

public class PlayerCollisions2D : MonoBehaviour
{
    public IDamageable2D damageable;

    private void Start()
    {
        damageable = GetComponent<IDamageable2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            PlayerEvents.Finish();

            PlayerScore.instance.UpdateHighScore();
        }

        if (other.CompareTag("Collectible"))
        {
            PlayerScore.instance.AddScore(1);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            PlayerEvents.Hit();
            damageable.HealthChange(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !GetComponent<PlayerMovement2D>().IsGrounded())
        {
            Destroy(collision.gameObject);
        }
    }
}

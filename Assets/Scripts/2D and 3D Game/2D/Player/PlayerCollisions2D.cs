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
            PlayerHighScore.instance.TrySetHighScore(PlayerScore.instance.Score);
            PlayerEvents.Finish();
        }

        if (other.CompareTag("ScoreCollectible"))
        {
            PlayerScore.instance.AddScore(1);
        }

        if (other.CompareTag("HealthCollectible"))
        {
            PlayerEvents.CollectedHealthBerry(other.GetComponent<Collectible2D>().collectibleId);
            damageable.HealthChange(+1);
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

using UnityEngine;

public class PlayerAnimations2D : MonoBehaviour
{

    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    private PlayerMovement2D playerMovement;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement2D>();
    }

    private void OnEnable()
    {
        PlayerEvents.OnPlayerHit += HitAnimation;
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        playerAnimator.SetBool("isGrounded", playerMovement.IsGrounded());

        if (playerMovement.MoveInput().x > 0)
        {
            playerSprite.flipX = false;
        }
        else if (playerMovement.MoveInput().x < 0)
        {
            playerSprite.flipX = true;
        }

        if (playerMovement.MoveInput().x != 0)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }

    private void HitAnimation()
    {
        playerAnimator.SetTrigger("Hit");
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerHit -= HitAnimation;
    }
}

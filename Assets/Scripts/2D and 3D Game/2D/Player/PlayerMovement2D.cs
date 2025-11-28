using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    private IPauseService pauseService;

    [SerializeField] private GameObject groundCheck;
    private Rigidbody2D playerRB;
    public LayerMask groundLayer;

    public float moveSpeed = 5f;
    public float gravity = -10f;
    public float jumpForce = 10f;
    public float jumpHoldTime = 0.1f;

    private float speed;
    private float gravityForce;

    private bool isJumping;
    private bool jumpPressed;
    private bool jumpHeld;
    private float jumpTimeCounter;

    private Vector2 moveInput;
    private Vector2 moveInputVector;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        pauseService = GetComponent<IPauseService>();
    }

    private void Start()
    {
        gravityForce = gravity * 10;
    }

    private void Update()
    {
        if (pauseService.paused)
        {
            moveInputVector = Vector3.zero;
        }
        else
        {
            moveInputVector = moveInput;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        speed = moveSpeed * 10;

        HandleJump();

        playerRB.AddForce(new Vector2(moveInputVector.x * speed, gravityForce));

        Vector2 currentVelocity = playerRB.linearVelocity;
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -moveSpeed, moveSpeed);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, gravity, -gravity / 2);

        playerRB.linearVelocity = currentVelocity;
        playerRB.linearVelocity = Vector2.Lerp(playerRB.linearVelocity, new Vector2(0, playerRB.linearVelocity.y), Time.fixedDeltaTime * 15f);
    }

    public void HandleJump()
    {
        if ((jumpPressed && IsGrounded()))
        {
            isJumping = true;
            jumpTimeCounter = jumpHoldTime;
            jumpPressed = false;
        }

        if (jumpHeld && isJumping && jumpTimeCounter > 0)
        {
            playerRB.AddForce(new Vector2(0, jumpForce * 100));
            jumpTimeCounter -= Time.fixedDeltaTime;
            gravityForce = 0;
        }

        if (jumpHeld == false || jumpTimeCounter <= 0)
        {
            isJumping = false;
            gravityForce = gravity * 10;
        }
    }

    public bool IsGrounded()
    {
        var a = Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(1.1f, 0.1f), 0f, groundLayer);
        if (a != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 MoveInput() => moveInputVector;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.performed;
        jumpHeld = context.performed;
    }
}

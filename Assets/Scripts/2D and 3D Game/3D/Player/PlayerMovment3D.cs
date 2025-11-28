using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment3D : MonoBehaviour
{
    public IPauseService pauseService;
    private Rigidbody playerRB;

    public float speed = 5f;
    public float maxSpeed = 10f;

    private Vector2 moveInput;
    private Vector2 moveInputVector;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        pauseService = GetComponent<IPauseService>();
    }

    private void Update()
    {
        if (pauseService.paused)
        {
            moveInputVector = Vector2.zero;
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
        playerRB.AddForce((moveInputVector.y * speed * transform.forward) + (moveInputVector.x * (speed / 2) * transform.right));
        playerRB.maxLinearVelocity = maxSpeed;
        playerRB.linearVelocity = Vector3.Lerp(playerRB.linearVelocity, Vector3.zero, Time.deltaTime * 10f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

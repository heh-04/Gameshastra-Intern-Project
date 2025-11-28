using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation3D : MonoBehaviour
{
    public float mouseSensitivity = 2f;

    private Vector2 lookInput;
    private Vector2 lookInputVector;

    private IPauseService pauseService;

    private void Awake()
    {
        pauseService = GetComponent<IPauseService>();
    }

    private void Update()
    {
        if (pauseService.paused)
        {
            lookInputVector = Vector2.zero;
        }
        else
        {
            lookInputVector = lookInput;
        }
    }

    private void LateUpdate()
    {
        RotationControl();
    }

    public void RotationControl()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0f, lookInputVector.x * mouseSensitivity, 0f), 1);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}

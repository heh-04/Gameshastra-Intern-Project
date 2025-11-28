
using UnityEngine.InputSystem;

public interface IPauseService
{
    bool paused { get; set; }

    void PauseToggle();

    void OnPause(InputAction.CallbackContext context);
}

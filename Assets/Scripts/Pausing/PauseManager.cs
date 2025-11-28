using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour, IPauseService
{
    public bool paused { get; set; }

    private void OnEnable()
    {
        PlayerEvents.OnPlayerFinish += HandleGameOver;
        PlayerEvents.OnPlayerDeath += HandleGameOver;
    }

    private void Start()
    {
        paused = true;
        PauseToggle();
    }


    public void PauseToggle()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed && !UI_Manager.instance.gameOverUI.activeSelf)
        {
            PauseToggle();
            UI_Manager.instance.pausedUI.SetActive(paused && !UI_Manager.instance.gameOverUI.activeSelf);
        }
    }

    private void HandleGameOver()
    {
        PauseToggle();
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerFinish -= HandleGameOver;
        PlayerEvents.OnPlayerDeath -= HandleGameOver;
    }
}

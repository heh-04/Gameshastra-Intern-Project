using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScoreText;
    public GameObject gameOverUI;
    public GameObject pausedUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        PlayerEvents.OnPlayerDeath += HandleGameOverUI;
        PlayerEvents.OnPlayerFinish += HandleFinishUI;
        PlayerScore.OnScoreChanged += UpdateScoreUI;
        PlayerScore.OnHighScoreChanged += UpdateHighScoreUI;
    }

    private void Start()
    {
        gameOverUI.SetActive(false);
        pausedUI.SetActive(false);
    }

    private void HandleGameOverUI()
    {
        gameOverUI.SetActive(true);
        gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over!";
    }

    private void HandleFinishUI()
    {
        gameOverUI.SetActive(true);
        gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Finished!";
    }

    private void UpdateScoreUI(float score)
    {
        scoreText.text = score.ToString();
        finalScoreText.text = "Score: " + score.ToString();
    }

    private void UpdateHighScoreUI(float highScore)
    {
        highScoreText.text = "Highscore: " + highScore.ToString();
    }

    public void LoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerDeath -= HandleGameOverUI;
        PlayerEvents.OnPlayerFinish -= HandleFinishUI;
        PlayerScore.OnScoreChanged -= UpdateScoreUI;
        PlayerScore.OnHighScoreChanged -= UpdateHighScoreUI;
    }
}

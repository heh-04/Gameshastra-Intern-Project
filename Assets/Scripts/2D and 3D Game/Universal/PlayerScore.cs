using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;

    public static event Action<float> OnScoreChanged;
    public static event Action<float> OnHighScoreChanged;

    public static float Score { get; private set; }
    public static float HighScore { get; private set; }

    private string highScoreKey;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D Game"))
        {
            highScoreKey = "Highscore2D";
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("3D Game"))
        {
            highScoreKey = "Highscore3D";
        }

        HighScore = PlayerPrefs.GetFloat(highScoreKey, 0f);
        OnHighScoreChanged?.Invoke(HighScore);

        ResetScore();
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    public void AddScore(float amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    public void UpdateHighScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            OnHighScoreChanged?.Invoke(HighScore);
            PlayerPrefs.SetFloat(highScoreKey, HighScore);
            PlayerPrefs.Save();
        }
    }
}

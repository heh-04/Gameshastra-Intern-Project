using System;
using UnityEngine;

public class PlayerHighScore : MonoBehaviour
{
    public static PlayerHighScore instance;
    public float HighScore { get; private set; }
    public static event Action<float> OnHighScoreChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadHighScore(float value)
    {
        HighScore = value;
        OnHighScoreChanged?.Invoke(HighScore);
    }

    public void TrySetHighScore(float score)
    {
        if(score > HighScore)
        {
            HighScore = score;
            OnHighScoreChanged?.Invoke(HighScore);
        }
    }
}

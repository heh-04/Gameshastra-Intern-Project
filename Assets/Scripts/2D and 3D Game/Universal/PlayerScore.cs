using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;

    public float Score { get; private set; }
    public static event Action<float> OnScoreChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
}

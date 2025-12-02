using UnityEngine;

public class G : MonoBehaviour
{
    float highScore;

    private void OnEnable()
    {
        PlayerEvents.OnPlayerFinish += SaveGame;
    }

    private void Start()
    {
        LoadGame();
        PlayerScore.instance.ResetScore();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerHighScore2D = PlayerHighScore.instance.HighScore;

        SaveSystem2D.Save(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem2D.Load();

        if (data != null)
        {
            PlayerHighScore.instance.LoadHighScore(data.playerHighScore2D);
        }
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerFinish -= SaveGame;
    }
}

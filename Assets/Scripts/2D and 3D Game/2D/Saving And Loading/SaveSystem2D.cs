using System.IO;
using UnityEngine;

public static class SaveSystem2D
{
    private static string path = Application.persistentDataPath + "/GameSaveData.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);
    }

    public static SaveData Load()
    {
        if (!File.Exists(path))
        {
            Debug.Log("No Save Found"); 
            return null;
        }
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static void DeleteSave()
    {
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }
}

[System.Serializable]
public class SaveData
{
    public float playerHighScore2D;
}

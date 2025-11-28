using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [NonSerialized] public string sceneToLoad;

    public static Action<string> buttonId;

    private void OnEnable()
    {
        buttonId += OnButtonPressed;
    }

    private void OnButtonPressed(string sceneToLoad)
    {
        if (sceneToLoad == "Exit")
        {
            Debug.Log("Exit!");
            Application.Quit();
        }
        else
        {
            LoadScene(sceneToLoad);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    private void OnDisable()
    {
        buttonId -= OnButtonPressed;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private LoadingScreen loadingScreen;
    public bool allowSceneSwitch = false;
    public Slider slider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (loadingScreen == null)
        {
            loadingScreen = FindAnyObjectByType<LoadingScreen>();
            LoadScene();
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(loadingScreen.sceneToLoad))
        {
            StartCoroutine(LoadAsync(loadingScreen.sceneToLoad));
        }
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        allowSceneSwitch = false;
        operation.allowSceneActivation = allowSceneSwitch;

        while (!operation.isDone)
        {
            slider.value = Mathf.Clamp01(operation.progress / 0.9f);

            if (operation.progress >= 0.9f)
            {
                slider.value = 1f;
                operation.allowSceneActivation = allowSceneSwitch;
            }
            yield return null;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    public string sceneToLoad;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed() => LoadingScreen.buttonId(sceneToLoad);
}

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    
    [SerializeField]
    private Canvas mainCanvas, settingCanvas;

    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    
    void Start()
    {
        ShowMainMenu();
        HideSettingsMenu();
    }
    
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
        HideMainMenu();
    }
    
    public void OpenSettings()
    {
        HideMainMenu();
        ShowSettingMenu();
    }
    
    public void OpenStore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        HideMainMenu();
    }

    public void BackMain()
    {
        ShowMainMenu();
        HideSettingsMenu();
    }

    //SettingCanvas
    void ShowSettingMenu()
    {
        settingCanvas.enabled = true;
    }
    void HideSettingsMenu()
    {
        settingCanvas.enabled = false;
    }
    
    //MainCanvas
    void ShowMainMenu()
    {
        mainCanvas.enabled = true;
    }
    void HideMainMenu()
    {
        mainCanvas.enabled = false;
    }
}

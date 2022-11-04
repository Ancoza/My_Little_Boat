using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    
    [SerializeField]
    private Canvas mainCanvas, settingCanvas, gameCanvas;

    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    
    void Start()
    {
        MainMenu();
    }
    
    public void InGame()
    {
        ShowGameMenu();
        HideMainMenu();
    }
    
    public void OpenSettings()
    {
        ShowSettingMenu();
    }

    public void CloseSettings()
    {
        HideSettingsMenu();
    }
    public void OpenStore()
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
        HideMainMenu();
    }

    public void MainMenu()
    {
        ShowMainMenu();
        HideGameMenu();
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
    
    //gameCanvas
    public void ShowGameMenu()
    {
        gameCanvas.enabled = true;
    }
    public void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }
}

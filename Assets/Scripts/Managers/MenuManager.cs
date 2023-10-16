using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    
    [SerializeField]
    private Canvas mainCanvas, settingCanvas, gameCanvas, gameOverCanvas;

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
    
    public void InGameMenu()
    {
        ShowGameMenu();
        HideMainMenu();
    }

    public void GameOver()
    {
        ShowGameOverMenu();
    }

    public void CloseGameOver()
    {
        HideGameOverMenu();
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
        HideGameOverMenu();
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
    void ShowGameMenu()
    {
        gameCanvas.enabled = true;
    }
    void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }
    
    //GameOver
    void ShowGameOverMenu()
    {
        gameOverCanvas.enabled = true;
    }

    void HideGameOverMenu()
    {
        gameOverCanvas.enabled = false;
    }
}

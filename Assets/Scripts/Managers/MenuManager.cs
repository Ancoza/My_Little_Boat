using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    
    [SerializeField]
    private Canvas mainCanvas, settingCanvas, gameCanvas, gameOverCanvas;
    [SerializeField]
    private Button btnPlay;
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    
    void Start()
    {
        MainMenu();
    }
    
    public void MainMenu()
    {
        ShowMainMenu();
        HideGameMenu();
        HideSettingsMenu();
        HideGameOverMenu();
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
        btnPlay.interactable = true;
    }
    void HideMainMenu()
    {
        mainCanvas.enabled = false;
        btnPlay.interactable = false;
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

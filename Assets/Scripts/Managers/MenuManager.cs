using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    
    [SerializeField]
    private Canvas mainCanvas, settingCanvas, gameCanvas, gameOverCanvas, instructionsCanvas;
    [SerializeField]
    private Button btnPlay;
    [SerializeField]
    private Toggle instructions;
    private int hide = 0;
    
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
        hide = PlayerPrefs.GetInt("hideInstructions");
        Debug.Log(hide);
        if (hide==1)
        {
            HideInstructionsCanvas();
        }
        else
        {
            
            ShowInstructionsCanvas();
        }
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

    public void CloseInstructions()
    {
        HideInstructionsCanvas();
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
    //Instructions
    void ShowInstructionsCanvas()
    {
        instructionsCanvas.enabled = true;
    }
    void HideInstructionsCanvas()
    {
        instructionsCanvas.enabled = false;
    }

    public void instuctions()
    {
        PlayerPrefs.SetInt("hideInstructions", 1);
    }
}

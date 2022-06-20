using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    [SerializeField]
    private Canvas gameCanvas, storeCanvas, settingCanvas, mainCanvas;

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
        HideGameCanvas();
        HideStoreCanvas();
        HideSettingsMenu();
    }
    
    void Update()
    {
        
    }

    public void OpenSettings()
    {
        HideMainMenu();
        ShowSettingMenu();
    }

    public void OpenStore()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        /*
        HideMainMenu();
        ShowStoreCanvas();
        */
    }

    public void Play()
    {
        HideMainMenu();
        ShowGameCanvas();
    }

    public void BackMain()
    {
        ShowMainMenu();
        HideGameCanvas();
        HideStoreCanvas();
        HideSettingsMenu();
    }
    
    //Game Canvas
    public void ShowGameCanvas()
    {
        gameCanvas.enabled = true;
    }
    public void HideGameCanvas()
    {
        gameCanvas.enabled = false;
    }
    
    //StoreCanvas
    public void ShowStoreCanvas()
    {
        storeCanvas.enabled = true;
    }
    public void HideStoreCanvas()
    {
        storeCanvas.enabled = false;
    }
    
    //SettingCanvas
    public void ShowSettingMenu()
    {
        settingCanvas.enabled = true;
    }
    public void HideSettingsMenu()
    {
        settingCanvas.enabled = false;
    }
    
    //MainCanvas
    public void ShowMainMenu()
    {
        mainCanvas.enabled = true;
    }
    public void HideMainMenu()
    {
        mainCanvas.enabled = false;
    }
}

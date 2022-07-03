using UnityEngine;

public class MenuGame : MonoBehaviour
{
    public static MenuGame SharedInstance;
    
    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
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

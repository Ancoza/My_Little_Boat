using UnityEngine;
using UnityEngine.UI;

public class DEVOPS : MonoBehaviour
{
    public Toggle ToggleDEVOPS;
    public Canvas CanvasDEVOPS;
    private void Start()
    {
        ToggleDEVOPS.isOn = false;
        HideCanvas();
    }
    
    public void ToggleCanvas()
    {
        if (ToggleDEVOPS.isOn)
        {
            ShowCanvas();
        }
        else
        {
            HideCanvas();
        }
    }
    void ShowCanvas()
    {
        CanvasDEVOPS.enabled = true;
    }
    void HideCanvas()
    {
        CanvasDEVOPS.enabled = false;
    }
}

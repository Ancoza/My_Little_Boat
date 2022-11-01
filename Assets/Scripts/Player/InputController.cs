using UnityEngine;

public class InputController : MonoBehaviour
{
    public void ChangeToGyroscope()
    {
        PlayerPrefs.SetInt("GameController",0);
    }

    public void ChangeToTouch()
    {
        PlayerPrefs.SetInt("GameController",1);
    }
}

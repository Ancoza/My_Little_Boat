using System;
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    private Transform currentView;
    public Transform viewDeath;
    public Transform viewMain;
    public Transform viewGame;
    
    public float transitionSpeed;

    private Vector3 camPosition;

    private void Start()
    {
        currentView = transform;
    }

    private void LateUpdate()
    {
        if (GameManager.SharedInstance.currentGameState == GameState.Menu)
        {
            transform.position = Vector3.Lerp(transform.position, viewMain.position, transitionSpeed);
            var rotation = viewMain.transform.rotation;
            
            Vector3 currentAngle = new Vector3(
                Mathf.Lerp(transform.rotation.eulerAngles.x, rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(transform.rotation.eulerAngles.y, rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                0
            );
            transform.eulerAngles = currentAngle;
        }
        else if(GameManager.SharedInstance.currentGameState == GameState.InGame)
        {
            transform.position = Vector3.Lerp(transform.position, viewGame.position, Time.deltaTime * 2f);
            
            Vector3 currentAngle = new Vector3(
                Mathf.Lerp(transform.rotation.eulerAngles.x, viewGame.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(transform.rotation.eulerAngles.y, viewGame.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                0
            );
            transform.eulerAngles = currentAngle;
            
        }else if(GameManager.SharedInstance.currentGameState == GameState.GameOver)
        {
            transform.position = Vector3.Lerp(transform.position, viewDeath.position, transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.Lerp(transform.rotation.eulerAngles.x, viewDeath.transform.rotation.eulerAngles.x,
                    Time.deltaTime * transitionSpeed),
                0, 0
            );
            transform.eulerAngles = currentAngle;
        }
        else
        {
            camPosition = new Vector3(viewDeath.transform.position.x, 2, -5);
            transform.position = Vector3.Lerp(transform.position, camPosition, transitionSpeed);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    private Transform currentView;
    public Transform viewDeth;
    public float transitionSpeed;

    private Vector3 camPosition;

    private void LateUpdate()
    {
        if (GameManager.SharedInstance.currentGameState == GameState.GameOver)
        {
            transform.position = Vector3.Lerp(transform.position, viewDeth.position, transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.Lerp(transform.rotation.eulerAngles.x, viewDeth.transform.rotation.eulerAngles.x,
                    Time.deltaTime * transitionSpeed),
                0, 0
            );
            transform.eulerAngles = currentAngle;
        }
        else
        {
            camPosition = new Vector3(viewDeth.transform.position.x, 2, -5);
            transform.position = Vector3.Lerp(transform.position, camPosition, transitionSpeed);
        }
        
    }
}

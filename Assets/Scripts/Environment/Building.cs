using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform finishPosition;
    
    float _speed = 0;
    
    private void Update()
    {
        if (GameManager.SharedInstance.currentGameState == GameState.InGame)
        {
            _speed = GameManager.SharedInstance.GetGameVelocity();
            transform.Translate(Vector3.back * (_speed * Time.deltaTime));
            if (transform.position.z <= -6)
            {
                Environment.SharedInstance.DeleteBuilding();
                Environment.SharedInstance.AddBuilding();
            }
        }
        else
        {
            _speed = 0;
        }
    }
}

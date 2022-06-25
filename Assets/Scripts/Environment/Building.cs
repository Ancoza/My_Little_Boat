using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform startPosition, finishPosition;
    
    float _speed;
    
    private void Update()
    {
        _speed = GameManager.SharedInstance.GetGameVelocity();
        
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
        
        if (transform.position.z <= -6)
        {
            Environment.SharedInstance.DeleteBuilding();
            Environment.SharedInstance.AddBuilding();
        }
    }
}

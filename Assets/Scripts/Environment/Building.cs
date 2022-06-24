using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform startPosition, finishPosition;
    
    float speed = 5;
    
    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        
        if (transform.position.z <= -6)
        {
            Environment.SharedInstance.DeleteBuilding();
            Environment.SharedInstance.AddBuilding();
        }
    }
}

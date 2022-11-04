using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform finishPosition;
    
    private void Update()
    {
        if (transform.position.z <= -10)
        {
            Environment.SharedInstance.DeleteBuilding();
            Environment.SharedInstance.AddBuilding();
        }
    }
}

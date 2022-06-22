using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform startPosition, finishPosition;
    private void Update()
    {
        if (transform.position.z <= -6)
        {
            Environment.SharedInstance.DeleteBuilding();
            Environment.SharedInstance.AddBuilding();
        }
    }
}

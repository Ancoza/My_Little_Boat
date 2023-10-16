using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform finishPosition;
    const int POINT_DESTRUCTION = -10;
    private void Update()
    {
        if (transform.position.z <= POINT_DESTRUCTION)
        {
            Environment.SharedInstance.DeleteBuildings();
            Environment.SharedInstance.AddBuildings();
        }
    }
}

using UnityEngine;

public class Boat : MonoBehaviour
{
    public int id;
    public string description;
    public int price;
    public bool isUnlock;
    public bool onUse;

    void Start()
    {
        SaveSystem.SaveBoat(this);
    }
    
    
}

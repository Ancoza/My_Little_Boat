[System.Serializable]
public class BoatData
{
    public int id;
    public string description;
    public int price;
    public bool isUnlock;
    public bool onUse;

    public BoatData(Boat boat)
    {
        id = boat.id;
        description = boat.description;
        price = boat.price;
        isUnlock = boat.isUnlock;
        onUse = boat.onUse;
    }
}

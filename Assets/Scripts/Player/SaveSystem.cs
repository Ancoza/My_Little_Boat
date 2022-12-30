using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static readonly string Path = Application.persistentDataPath + "/player.fun";
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Path, FileMode.Open);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
            
        player.ResetData();
    }

    public static void SaveBoat(Boat boat)
    {
        string path = Application.persistentDataPath + "/boatData" + boat.id + ".anc";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        BoatData data = new BoatData(boat);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static PlayerData LoadPlayer()
    {
        //Open file and creat formatter
        FileStream stream = new FileStream(Path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        
        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();
        return data;
    }
    
    public static void Delete()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }

    private static int ExistData()
    {
        if (File.Exists(Path))
        {
            FileStream stream = new FileStream(Path, FileMode.Open);
            
            switch (stream.Length)
            {
                case 0:
                    //File Empty
                    stream.Close();
                    return 1;
                default:
                    //File contains player data
                    stream.Close();
                    return 2;
            }
        }
        //File don't exist
        return 3;
    }

    public static void InitializedData()
    {
        if (ExistData() == 1 || ExistData() == 3)
        {
            FileStream stream = new FileStream(Path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = new PlayerData();
            
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static string CreateBoatPath(int id)
    {
        string path = Application.persistentDataPath + "/boatData" + id + ".anc";
        return path;
    }
    
}

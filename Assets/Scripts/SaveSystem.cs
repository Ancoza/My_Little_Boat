using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Net.NetworkInformation;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";

        if (!File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            player.coins += LoadPlayer().coins;
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
            player.coins = 0;
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;
        }
        else
        {
            return null;
        }
        
    }

}

using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;

public static class SaveSystem
{
    
    //private static string PATH = Application.persistentDataPath + "/player.fun";
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";

        if (!File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(player);
            //formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
            
            player.NewData();
        }
    }
    
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        
        //Open file and creat formatter
        FileStream stream = new FileStream(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        
        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();
        return data;
    }
    
    public static void Delete()
    {
        string path = Application.persistentDataPath + "/player.fun";
        
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static int ExistData()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            switch (stream.Length)
            {
                case 0:
                    //El archivo esta vacio
                    stream.Close();
                    return 1;
                default:
                    //El archivo contiene datos del jugado
                    stream.Close();
                    return 2;
            }
        }
        //El archivo no existe
        return 3;
    }

    public static void InitializedData()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (ExistData() == 1 || ExistData() == 3)
        {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = new PlayerData();
        
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

}

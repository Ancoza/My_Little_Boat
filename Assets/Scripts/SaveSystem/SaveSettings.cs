using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

public static class SaveSettings
{
    private static readonly string Path = Application.persistentDataPath + "/settigns.acz";

    public static void SaveGameSettings(Settings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Path, FileMode.Open);
        DataSettings dataSettigns = new DataSettings(settings);
        formatter.Serialize(stream, dataSettigns);
        stream.Close();
    }

    public static DataSettings LoadGameSettings()
    {
        InitializedData();
        //Open file and creat formatter
        FileStream stream = new FileStream(Path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        DataSettings dataSettings = formatter.Deserialize(stream) as DataSettings;
        Debug.Log(dataSettings.GetGeneralVolume());
        stream.Close();
        return dataSettings;
    }

    private static bool ExistData()
    {
        if (File.Exists(Path))
        {
            FileStream stream = new FileStream(Path, FileMode.Open);
            switch (stream.Length)
            {
                case 0:
                    //File Empty
                    stream.Close();
                    Debug.Log("Archivo Vacio");
                    return false;
                default:
                    //File contains player data
                    stream.Close();
                    Debug.Log("Archivo Con datos");
                    return true;
            }
        }
        else
        {
            Debug.Log("Archivo No existe");
            return false;
        }
    }

    public static void InitializedData()
    {
        if (!ExistData())
        {
            FileStream stream = new FileStream(Path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            DataSettings dataSettings = new DataSettings();

            formatter.Serialize(stream, dataSettings);
            stream.Close();
        }
    }
}

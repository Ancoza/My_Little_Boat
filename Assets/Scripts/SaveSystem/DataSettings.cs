using UnityEngine;

[System.Serializable]
public class DataSettings
{
    private GameController controller;
    private float generalVolume;
    private bool volumeFX;
    private bool volumeMusic;

    public DataSettings() 
    {
        controller = GameController.Gyroscope;
        generalVolume = 1.0f;
        volumeFX = true;
        volumeMusic = true;
    }

    public DataSettings(Settings settings)
    {
        controller = settings.GetController();
        generalVolume = settings.GetGeneralVolume();
        volumeFX = settings.GetVolumeFX();
        volumeMusic = settings.GetVolumeMusic();
    }

    public GameController GetController() { return controller; }
    public float GetGeneralVolume() { return generalVolume; }
    public bool GetVolumeFX() { return volumeFX; }
    public bool GetVolumeMusic() { return volumeMusic; }
}

using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Settings
{
    private GameController controller;
    private float generalVolume;
    private bool volumeFX;
    private bool volumeMusic;

    public Settings() 
    {
        
    }
    public void SaveGameSettigns()
    {
        SaveSettings.SaveGameSettings(this);
    }

    public void LoadGameSettigns()
    {
        DataSettings dataSettings = SaveSettings.LoadGameSettings();
        controller = dataSettings.GetController();
        generalVolume = dataSettings.GetGeneralVolume();
        volumeFX = dataSettings.GetVolumeFX();
        volumeMusic = dataSettings.GetVolumeMusic();
    }

    public GameController GetController(){ return controller; }
    public float GetGeneralVolume(){ return generalVolume; }
    public bool GetVolumeFX () { return volumeFX; }
    public bool GetVolumeMusic() { return volumeMusic; }

    public void SetVolumeFX(bool value) {  volumeFX = value; }
    public void SetVolumeMusic(bool value) { volumeMusic = value; }
    public void SetGeneralVolume(float value) { generalVolume = value; }
    public void SetController(GameController controller) { this.controller = controller; }
}

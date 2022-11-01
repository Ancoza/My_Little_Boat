using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup effectsGroup;

    public Toggle music;
    public Toggle effects;

    public void ChangeVolume()
    {
        if (music.isOn)
        {
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public string channel;
    private MusicManager manager;

    void Start()
    {
        manager = MusicManager.Instance;
        UpdateSlider();
    }
    public void UpdateVolume()
    {
        manager.MasterVolume(channel, GetComponent<Slider>().value);
    }
    public void UpdateSlider()
    {
        switch(channel)
        {
            case "SFXMasterVolume":
                GetComponent<Slider>().value = MusicManager.sfxVolume;
                break;

            case "MusicMasterVolume":
                GetComponent<Slider>().value = MusicManager.musicVolume;
                break;

            default:
                Debug.Log("unspecified audiosliderchannel");
                break;
        }
    }
}

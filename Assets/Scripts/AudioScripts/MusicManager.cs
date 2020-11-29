using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [Range(0,2)]public static float musicVolume = 1f;
    [Range(0,2)]public static float sfxVolume = 1f;
    public static MusicManager instance = null;
    public float transitionDuration = 2f;
    //public float uiMusicTransitionDuration = 1f;
    
    // [Range(0,3)]public int intensity = 0;
    public bool isInside = true;
    
    // public bool isUIMusic = false;
   
    public AudioMixer mixer;

    private float[] vol;

    // private float volG = 0f;
    // private float volUI = 0f;

    // void OnValidate()
    // {
    //    Transition(intensity);
    // }
    
    void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }

        vol = new float[2];
    }

    void Start()
    {
        MasterVolume("s", sfxVolume);
        MasterVolume("m", musicVolume);
    }

    public void Transition(bool goingInside)
    {
        isInside = goingInside;
    }

    // public void TransitionToDominantSong()
    // {
    //     isUIMusic = false;
    //     Debug.Log("Gameplay Music...");
    // }
    // public void TransitionToUISong()
    // {
    //     Debug.Log("UI Music...");
    //     isUIMusic = true;
        
    // }

    // private void MasterMusicVolume(float volume)
    // {
    //     mixer.SetFloat("MusicMasterVolume", RemapVolumePercent(volume));
    //     musicVolume = volume;
    // }
    public void MasterVolume(string volumeType, float volume)
    {
        switch(volumeType)
        {
            case "s":
                sfxVolume = volume;
                break;

            case "m":
                musicVolume = volume;
                break;

            default:
                Debug.Log("unspecified audiochannel");
                break;
        }
        mixer.SetFloat(volumeType, RemapVolumePercent(volume));
    }

    private float RemapVolumePercent(float percent)
    {
        float newValue = 0f;
        percent = Mathf.Clamp(percent, 0.001f, 1f);
        newValue = Mathf.Log(percent) * 20;
        return newValue;
    }

    void Update()
    {
        // for(int i = 0; i<4; i++)
        // {
        //     if(i == intensity)
        //         vol[i] = Mathf.Clamp(vol[i] + Time.unscaledDeltaTime / transitionDuration, 0f,1f);
        //     else
        //         vol[i] = Mathf.Clamp(vol[i] - Time.unscaledDeltaTime / transitionDuration, 0f,1f);
        //     mixer.SetFloat("M_"+i+"_Vol", RemapVolumePercent(vol[i]));
        // }

        // if(isUIMusic)
        // {
        //     volG = Mathf.Clamp(volG - Time.unscaledDeltaTime / uiMusicTransitionDuration,0f,1f);
        //     volUI = Mathf.Clamp(volUI + Time.unscaledDeltaTime / uiMusicTransitionDuration,0f,1f);
        // }
        // else
        // {
        //     volG = Mathf.Clamp(volG + Time.unscaledDeltaTime / uiMusicTransitionDuration,0f,1f);
        //     volUI = Mathf.Clamp(volUI - Time.unscaledDeltaTime / uiMusicTransitionDuration,0f,1f);
        // }
        // mixer.SetFloat("G_MusicVolume", RemapVolumePercent(volG));
        // mixer.SetFloat("UI_MusicVolume", RemapVolumePercent(volUI));
    }
}

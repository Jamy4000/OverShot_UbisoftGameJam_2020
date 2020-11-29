using System.Collections;
using System.Collections.Generic;
using UbiJam.Utils;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoSingleton<MusicManager>
{
    [Range(0,2)]public static float musicVolume = 1f;
    [Range(0,2)]public static float sfxVolume = 1f;
    public float transitionDuration = 2f;
    // public float uiMusicTransitionDuration = 1f;
    
    // [Range(0,3)]public int intensity = 0;
    //songindex: 0=menu, 1=inside, 2=outside
    [Range(0,2)]public int songIndex = 0;
    
    // public bool isUIMusic = false;
   
    public AudioMixer mixer;

    //store current volumes for each song
    private float[] vol;

    
    protected override void Awake()
    {
		base.Awake();
        vol = new float[3];

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        MasterVolume("s", sfxVolume);
        MasterVolume("m", musicVolume);
    }

    public void Transition(int newSongIndex)
    {
        songIndex = newSongIndex;
    }


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
        for(int i = 0; i<vol.Length; i++)
        {
           if(i == songIndex)
                vol[i] = Mathf.Clamp(vol[i] + Time.unscaledDeltaTime / transitionDuration, 0f,1f);
            else
                vol[i] = Mathf.Clamp(vol[i] - Time.unscaledDeltaTime / transitionDuration, 0f,1f);
            //mixer.SetFloat("M_"+i+"_Vol", RemapVolumePercent(vol[i]));
            switch(i)
            {
                case 0:
                    mixer.SetFloat("m_menu", RemapVolumePercent(vol[i]));
                    break;

                case 1:
                    mixer.SetFloat("m_inside", RemapVolumePercent(vol[i]));
                    break;
                
                case 2:
                    mixer.SetFloat("m_outside", RemapVolumePercent(vol[i]));
                    break;

                default:
                    break;
            }
         }
    }
}

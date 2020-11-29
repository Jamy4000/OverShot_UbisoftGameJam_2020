using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ModifyPitch : MonoBehaviour
{
    public float minPitch = 0.85f;
    public float maxPitch = 1.15f;
    void Awake()
    {
        ModifyPitchRandomly();
    }

    // public void onloop()
    // {
        
    // }

    void ModifyPitchRandomly()
    {
        GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
    }
}

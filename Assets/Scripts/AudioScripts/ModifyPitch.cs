using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyPitch : MonoBehaviour
{
    public float minPitch = 0.85f;
    public float maxPitch = 1.15f;
    void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
    }
}

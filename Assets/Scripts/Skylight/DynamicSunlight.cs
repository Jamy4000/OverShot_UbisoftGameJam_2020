using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSunlight : MonoBehaviour
{
    public float timeOfDay = 12.0f;
    public float dayDuration = 60.0f;
    public Gradient lightGradient;
    public AnimationCurve intensityCurve;
    public AnimationCurve rotationCurve;
    public AnimationCurve moonIntensityCurve;
    public bool autoAdvance = true;
    public Light moonlight;
    private Light light;

    void Update()
    {
        if(autoAdvance)
            AdvanceDayTime();
        UpdateLight();
    }

    void Awake()
    {
        light = GetComponent<Light>();
    }

    public void UpdateLight()
    {
        //update light rotation
        light.transform.localRotation = Quaternion.Euler(rotationCurve.Evaluate(timeOfDay/24),0,0);
        //update light color
        light.color = lightGradient.Evaluate(timeOfDay/24);
        moonlight.color = lightGradient.Evaluate(timeOfDay/24);
        //update light intensity
        light.intensity = intensityCurve.Evaluate(timeOfDay/24);
        moonlight.intensity = moonIntensityCurve.Evaluate(timeOfDay/24);
        
    }

    private void AdvanceDayTime()
    {
        float addedTime = Time.deltaTime / dayDuration * 24;
        timeOfDay += addedTime;
        timeOfDay %= 24;
    }

    private void SetTimeOfDay(float newTime)
    {
        timeOfDay = newTime%24;
    }

    void onValidate()
    {
        UpdateLight();
    }
}

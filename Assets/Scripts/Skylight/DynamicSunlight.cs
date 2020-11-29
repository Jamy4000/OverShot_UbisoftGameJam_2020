using UbiJam.Events;
using UbiJam.Gameplay;
using UnityEngine;

namespace UbiJam.Lighting
{
    public class DynamicSunlight : MonoBehaviour
    {
        public float timeOfDay = 12.0f;
        public float dayDuration = 60.0f;
        public Gradient lightGradient;
        public AnimationCurve intensityCurve;
        public AnimationCurve rotationCurve;
        public AnimationCurve moonIntensityCurve;
        public Light moonlight;
        private Light light;

        private void Awake()
        {
            OnGameStarted.Listeners += StartSystem;
            OnGameEnded.Listeners += EndSystem;
            light = GetComponent<Light>();
            UpdateLight();
            this.enabled = false;
        }

        private void Update()
        {
            if (!GameManager.Instance.IsRunning)
                return;

            AdvanceDayTime();
            UpdateLight();
        }

        private void OnDestroy()
        {
            OnGameStarted.Listeners -= StartSystem;
            OnGameEnded.Listeners -= EndSystem;
        }

        [ContextMenu("Update Light")]
        public void UpdateLight()
        {
            // for debug
            // light = GetComponent<Light>();
            //update light rotation
            light.transform.localRotation = Quaternion.Euler(rotationCurve.Evaluate(timeOfDay / 24), 0, 0);
            //update light color
            light.color = lightGradient.Evaluate(timeOfDay / 24);
            moonlight.color = lightGradient.Evaluate(timeOfDay / 24);
            //update light intensity
            light.intensity = intensityCurve.Evaluate(timeOfDay / 24);
            moonlight.intensity = moonIntensityCurve.Evaluate(timeOfDay / 24);

        }

        private void AdvanceDayTime()
        {
            float addedTime = Time.deltaTime / dayDuration * 24;
            timeOfDay += addedTime;
            timeOfDay %= 24;
        }

        private void SetTimeOfDay(float newTime)
        {
            timeOfDay = newTime % 24;
        }

        void onValidate()
        {
            UpdateLight();
        }

        private void StartSystem(OnGameStarted info)
        {
            this.enabled = true;
        }

        private void EndSystem(OnGameEnded info)
        {
            this.enabled = false;
        }
    }
}

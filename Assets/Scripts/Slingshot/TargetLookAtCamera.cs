using UbiJam.Events;
using UnityEngine;

namespace UbiJam.Slingshot
{
    public class TargetLookAtCamera : MonoBehaviour
    {
        private Transform _target;

        private void Awake()
        {
            OnCharacterReady.Listeners += DeactivateSystem;
            OnSlingshotReady.Listeners += ActivateSystem;
            _target = Camera.main.transform;
            this.enabled = false;
        }

        private void Update()
        {
            transform.LookAt(_target);
            transform.localRotation = Quaternion.Euler(0.0f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z); 
        }

        private void OnDestroy()
        {
            OnCharacterReady.Listeners -= DeactivateSystem;
            OnSlingshotReady.Listeners -= ActivateSystem;
        }

        private void ActivateSystem(OnSlingshotReady info)
        {
            this.enabled = true;
        }

        private void DeactivateSystem(OnCharacterReady info)
        {
            transform.localRotation = Quaternion.identity;
            this.enabled = false;
        }
    }
}
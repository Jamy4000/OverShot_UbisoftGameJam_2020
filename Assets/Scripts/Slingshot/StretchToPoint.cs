using UbiJam.Events;
using UnityEngine;

namespace UbiJam.Slingshot
{
    public class StretchToPoint : MonoBehaviour
    {
        [SerializeField]
        private float _lengthMultiplier = 1.0f;

        [SerializeField]
        private Transform _targetpoint;

        private void Awake()
        {
            OnCharacterReady.Listeners += DeactivateSystem;
            OnSlingshotReady.Listeners += ActivateSystem;
            this.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(_targetpoint);
            transform.localScale = new Vector3(1, 1, Vector3.Distance(transform.position, _targetpoint.position) * _lengthMultiplier);
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
            transform.localScale = Vector3.one;
            this.enabled = false;
        }
    }
}
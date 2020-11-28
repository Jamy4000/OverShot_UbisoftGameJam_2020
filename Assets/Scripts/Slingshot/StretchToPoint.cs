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

        private Vector3 _basePosition;
        private Quaternion _baseRotation;
        private Vector3 _baseScale;

        private void Awake()
        {
            OnSlingshotReady.Listeners += ActivateSystem;
            _baseRotation = transform.localRotation;
            _basePosition = transform.localPosition;
            _baseScale = transform.localScale;
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
            OnSlingshotReady.Listeners -= ActivateSystem;
        }

        private void ActivateSystem(OnSlingshotReady info)
        {
            this.enabled = true;
        }

        public void ResetPostion()
        {
            transform.localPosition = _basePosition;
            transform.localRotation = _baseRotation;
            transform.localScale = _baseScale;
            this.enabled = false;
        }
    }
}
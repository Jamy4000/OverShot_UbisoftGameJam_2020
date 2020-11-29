using UbiJam.Events;
using UnityEngine;

namespace UbiJam.Slingshot
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Transform _startPoint;

        private LineRenderer _renderer;

        private void Awake()
        {
            OnCharacterReady.Listeners += DeactivateSystem;
            OnSlingshotReady.Listeners += ActivateSystem;
            _renderer = GetComponent<LineRenderer>();
            _renderer.enabled = false;
            this.enabled = false;
        }

        private void Update()
        {
            _renderer.SetPosition(0, _startPoint.position);
            _renderer.SetPosition(1, _target.position);
        }

        private void OnDestroy()
        {
            OnCharacterReady.Listeners -= DeactivateSystem;
            OnSlingshotReady.Listeners -= ActivateSystem;
        }

        private void ActivateSystem(OnSlingshotReady info)
        {
            _renderer.enabled = true;
            this.enabled = true;
        }

        private void DeactivateSystem(OnCharacterReady info)
        {
            _renderer.enabled = false;
            this.enabled = false;
        }
    }
}
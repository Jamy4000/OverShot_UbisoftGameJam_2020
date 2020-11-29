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
        private Slingshot _slingshot;

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
            Vector3 direction = _slingshot.GetThrowForce();
            Vector3 previousPoint = _startPoint.position;
            _renderer.SetPosition(0, previousPoint);

            for (int i = 0; i < _renderer.positionCount; i++)
            {

            }
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
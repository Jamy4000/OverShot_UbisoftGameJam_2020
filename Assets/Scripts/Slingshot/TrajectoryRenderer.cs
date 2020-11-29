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
        [SerializeField]
        private float _pointDistance = 0.5f;

        private LineRenderer _renderer;
        private Slingshot _slingshot;
        private SlingshotSettings _slingshotSettings;

        private void Awake()
        {
            OnCharacterReady.Listeners += DeactivateSystem;
            OnSlingshotReady.Listeners += ActivateSystem;
            _renderer = GetComponent<LineRenderer>();
            _renderer.enabled = false;
            this.enabled = false;
        }

        private void Start()
        {
            _slingshotSettings = GameSettings.Instance.SlingshotSettings;
            _slingshot = Slingshot.Instance;
        }

        private void Update()
        {
            if (_slingshot.HasThrown)
                return;

            Vector3 direction = _slingshot.GetThrowForce();
            direction = direction.normalized;
            Vector3 previousPoint = _startPoint.position;

            Vector3 nextPoint;

            var distance = (_target.position - _startPoint.position).magnitude;
            var maxDistance = distance / (_slingshotSettings.MaxBackwardDistance - Vector3.Distance(Camera.main.transform.position, _startPoint.position));
            maxDistance = Mathf.Clamp01(maxDistance);
            _renderer.SetPosition(0, previousPoint);
            _slingshot.Points[0] = previousPoint;

            for (int i = 0; i < _renderer.positionCount; i++)
            {
                nextPoint = previousPoint + direction * _pointDistance;
                nextPoint += Physics.gravity * _slingshotSettings.GravityMultiplier * (1 - maxDistance) * i;
                _renderer.SetPosition(i, nextPoint);
                _slingshot.Points[i] = nextPoint;
                previousPoint = nextPoint;
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
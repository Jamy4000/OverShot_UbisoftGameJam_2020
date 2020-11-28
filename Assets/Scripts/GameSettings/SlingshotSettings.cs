using UnityEngine;

namespace UbiJam
{
    [CreateAssetMenu(menuName = "UbiJam/Create Slingshot Settings")]
    public class SlingshotSettings : ScriptableObject
    {
        [SerializeField]
        private float _playerWalkSpeed = 5.0f;
        public float PlayerWalkSpeed { get { return _playerWalkSpeed; } }

        [SerializeField]
        private Vector3 _cameraTargetPosition = new Vector3(0.0f, 1.8f, 0.0f);
        public Vector3 CameraTargetPosition { get { return _cameraTargetPosition; } }

        [SerializeField]
        private float _cameraSwitchSpeed = 5.0f;
        public float CameraSwitchFrameCount { get { return 100 * _cameraSwitchSpeed; } }

        [SerializeField]
        private AnimationCurve _slingshotWalkBackwardCurve = new AnimationCurve();
        public AnimationCurve SlingshotWalkBackwardCurve { get { return _slingshotWalkBackwardCurve; } }

        [SerializeField]
        private AnimationCurve _slingshotWalkForwardCurve = new AnimationCurve();
        public AnimationCurve SlingshotWalkForwardCurve { get { return _slingshotWalkForwardCurve; } }

        [SerializeField]
        private float _maxBackwardDistance = 3.0f;
        public float MaxBackwardDistance { get { return _maxBackwardDistance; } }

        [SerializeField]
        private float _minForwardDistance = 0.2f;
        public float MinForwardDistance { get { return _minForwardDistance; } }

        [SerializeField]
        private float _throwForce = 5.0f;
        public float ThrowForce { get { return _throwForce; } }

        [SerializeField]
        private Vector3 _forwardOffsetAttachPoint = new Vector3(0.0f, -1.0f, 1.2f);
        public Vector3 ForwardOffsetAttachPoint { get { return _forwardOffsetAttachPoint; } }

        [SerializeField]
        private float _gravityMultiplier = 0.5f;
        public float GravityMultiplier { get { return _gravityMultiplier; } }
    }
}

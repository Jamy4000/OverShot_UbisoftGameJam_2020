using UnityEngine;

namespace UbiJam
{
    [CreateAssetMenu(menuName = "UbiJam/Create Slingshot Settings")]
    public class SlingshotSettings : ScriptableObject
    {
        public float PlayerWalkSpeed = 5.0f;
        public Vector3 CameraTargetPosition = new Vector3(0.0f, 1.8f, 0.0f);

        [SerializeField]
        private float _cameraSwitchSpeed = 5.0f;
        public float CameraSwitchFrameCount { get { return 100 * _cameraSwitchSpeed; } }
    }
}

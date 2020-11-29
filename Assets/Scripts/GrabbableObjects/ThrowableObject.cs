using UbiJam.Events;
using UnityEngine;

namespace UbiJam.GrabbableObjects
{
    public class ThrowableObject : MonoBehaviour
    {
        private Vector3[] _points;
        public bool IsGettingThrown;
        private int _index;
        private int _speed = 2;

        private void Awake()
        {
            OnUserSwitchedController.Listeners += StopMovement;
        }

        private void OnDestroy()
        {
            OnUserSwitchedController.Listeners -= StopMovement;
        }

        private void StopMovement(OnUserSwitchedController info)
        {
            IsGettingThrown = false;
        }

        private void FixedUpdate()
        {
            if (IsGettingThrown)
            {
                transform.position = _points[_index];
                _index += 1 * _speed;

                if (_index >= _points.Length)
                    IsGettingThrown = false;
            }
        }

        public void ThrowThisObject(Vector3[] points)
        {
            _index = 0;
            _points = points;
            IsGettingThrown = true;
        }
    }
}

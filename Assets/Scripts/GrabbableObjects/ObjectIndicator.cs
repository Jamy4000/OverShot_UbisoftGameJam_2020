using UbiJam.Player;
using UnityEngine;

namespace UbiJam.GrabbableObjects
{
    public class ObjectIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _indicator;

        private ObjectGrabber _grabber;

        private void Start()
        {
            _grabber = ObjectGrabber.Instance;
            _indicator.SetActive(false);
        }

        private void Update()
        {
            if (!_indicator.activeSelf && _grabber.ClosestGrabbable != null)
            {
                _indicator.SetActive(true);
            }
            else if (_indicator.activeSelf && _grabber.ClosestGrabbable == null)
            {
                _indicator.SetActive(false);
            }
        }
    }
}

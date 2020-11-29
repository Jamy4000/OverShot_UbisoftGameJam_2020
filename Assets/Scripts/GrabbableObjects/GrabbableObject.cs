using UbiJam.Events;
using UbiJam.GrabbableObjects;
using UnityEngine;

namespace UbiJam.Slingshot
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrabbableObject : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public EGrabbableObjects _grabbableType;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            // TODO WHEN DONE WITH GAMR
            // gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Utils.TagsHolder.WindowTag))
            {
                collider.GetComponent<GrabbableReceiver>().OnObjectReceived(_grabbableType);
                ResetGrabbable();
            }
            else if (collider.CompareTag(Utils.TagsHolder.WallTag))
            {
                new OnSlingshotMiss();
                ResetGrabbable();
            }
        }

        public void ResetGrabbable()
        {
            gameObject.SetActive(false);
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}

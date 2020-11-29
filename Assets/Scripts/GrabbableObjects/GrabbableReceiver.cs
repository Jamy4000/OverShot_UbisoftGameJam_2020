using UbiJam.Gameplay;
using UnityEngine;

namespace UbiJam.GrabbableObjects
{
    public class GrabbableReceiver : MonoBehaviour
    {
        [SerializeField]
        private GameObject _neighborImage;
        [SerializeField]
        private UnityEngine.UI.Image _demandedObjectImage;

        private EGrabbableObjects DemandedObject;

        public bool IsActive { get; private set; } = false;

        private void Awake()
        {
            _neighborImage.SetActive(false);
        }

        public void OnObjectReceived(EGrabbableObjects receivedObject)
        {
            if (receivedObject == DemandedObject)
            {
                _neighborImage.SetActive(false);
                DemandedObject = EGrabbableObjects.None;
                GameManager.Instance.AddPoint(1);
            }
            else
            {
                Debug.Log("WRONG");
            }
            // TODO Add point or whatever
        }

        public void ActivateReceiver(GrabbableObjectType requestedItem)
        {
            IsActive = true;
            DemandedObject = requestedItem.Type;
            _demandedObjectImage.sprite = requestedItem.ItemSprite;
            _neighborImage.SetActive(true);
        }
    }
}

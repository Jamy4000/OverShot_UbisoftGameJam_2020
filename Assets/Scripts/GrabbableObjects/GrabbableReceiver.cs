using UnityEngine;

namespace UbiJam.GrabbableObjects
{
    public class GrabbableReceiver : MonoBehaviour
    {
        [SerializeField]
        private EGrabbableObjects DemandedObject;

        public void OnObjectReceived(EGrabbableObjects receivedObject)
        {
            if (receivedObject == DemandedObject)
            {
                Debug.Log("YOU GOT POINTS");
            }
            else
            {
                Debug.Log("WRONG");
            }
            // TODO Add point or whatever
        }
    }
}

using UbiJam.GrabbableObjects;

namespace UbiJam.Events
{
    public class OnObjectWasGrabbed : EventCallbacks.Event<OnObjectWasGrabbed>
    {
        public readonly EGrabbableObjects NewlyGrabbedObject;

        public OnObjectWasGrabbed(EGrabbableObjects newlyGrabbedObject) : base("Event raised when the user grabbed an object")
        {
            NewlyGrabbedObject = newlyGrabbedObject;
            FireEvent(this);
        }
    }
}

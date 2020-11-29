using UbiJam.GrabbableObjects;

namespace UbiJam.Events
{
    public class OnSlingshotHit : EventCallbacks.Event<OnSlingshotHit>
    {
        public readonly GrabbableReceiver Receiver;

        public OnSlingshotHit(GrabbableReceiver receiver) : base("Event raised when the user hit a target")
        {
            Receiver = receiver;
            FireEvent(this);
        }
    }
}

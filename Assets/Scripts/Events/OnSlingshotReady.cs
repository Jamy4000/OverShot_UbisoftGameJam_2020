namespace UbiJam.Events
{
    public class OnSlingshotReady : EventCallbacks.Event<OnSlingshotReady>
    {
        public OnSlingshotReady() : base("Event raised when the user can start using the slingshot")
        {
            FireEvent(this);
        }
    }
}

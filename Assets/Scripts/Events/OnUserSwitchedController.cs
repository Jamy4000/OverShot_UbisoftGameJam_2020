namespace UbiJam.Events
{
    public class OnUserSwitchedController : EventCallbacks.Event<OnUserSwitchedController>
    {
        public OnUserSwitchedController() : base("Event raised when the user switch between slingshot movement to normal movements")
        {
            FireEvent(this);
        }
    }
}

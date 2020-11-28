namespace UbiJam.Events
{
    public class OnUserSwitchedController : EventCallbacks.Event<OnUserSwitchedController>
    {
        public readonly bool IsSwitchingToSlingshot;

        public OnUserSwitchedController(bool isSwitchingToSlingshot) : base("Event raised when the user switch between slingshot movement to normal movements")
        {
            IsSwitchingToSlingshot = isSwitchingToSlingshot;
            FireEvent(this);
        }
    }
}

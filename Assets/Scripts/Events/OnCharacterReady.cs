namespace UbiJam.Events
{
    public class OnCharacterReady : EventCallbacks.Event<OnCharacterReady>
    {
        public OnCharacterReady() : base("Event raised when the user can start using the character")
        {
            FireEvent(this);
        }
    }
}

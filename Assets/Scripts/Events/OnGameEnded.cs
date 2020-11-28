namespace UbiJam.Events
{
    public class OnGameEnded : EventCallbacks.Event<OnGameEnded>
    {
        public OnGameEnded() : base("Event raised when the timer has reached 0")
        {
            FireEvent(this);
        }
    }
}

namespace UbiJam.Events
{
    public class OnGameStarted : EventCallbacks.Event<OnGameStarted>
    {
        public OnGameStarted() : base("Event raised when the countdown is done and the game can start")
        {
            FireEvent(this);
        }
    }
}

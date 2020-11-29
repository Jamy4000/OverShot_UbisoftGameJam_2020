namespace UbiJam.Events
    {
        public class OnSlingshotHit : EventCallbacks.Event<OnSlingshotHit>
        {
            public OnSlingshotHit() : base("Event raised when the user hit a target")
            {
                FireEvent(this);
            }
        }
    }

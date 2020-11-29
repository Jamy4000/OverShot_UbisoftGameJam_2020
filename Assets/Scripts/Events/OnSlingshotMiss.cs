namespace UbiJam.Events
    {
        public class OnSlingshotMiss : EventCallbacks.Event<OnSlingshotMiss>
        {
            public OnSlingshotMiss() : base("Event raised when the user miss a target")
            {
                FireEvent(this);
            }
        }
    }

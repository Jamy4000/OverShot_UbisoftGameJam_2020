using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public abstract class InputMapper
    {
        public bool IsEnable { get; protected set; }

        protected InputActionMap InputActionMap;

        public InputMapper(InputActionMap inputActionMap, bool isEnableOnStart)
        {
            InputActionMap = inputActionMap;

            if (isEnableOnStart)
                Enable();
            else
                Disable();
        }

        public abstract void Enable();
        public abstract void Disable();
    }
}

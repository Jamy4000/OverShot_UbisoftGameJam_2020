using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public class InputMapper : IEnabler
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

        public virtual void Enable()
        {
            IsEnable = true;
            InputActionMap.Enable();
        }

        public virtual void Disable()
        {
            IsEnable = false;
            InputActionMap.Disable();
        }
    }
}

using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public class SlingshotInputMapper : InputMapper
    {
        public readonly InputAction FireAction;
        public readonly InputAction LookAction;
        public readonly InputAction MoveAction;

        public SlingshotInputMapper(InputActionMap inputActionMap, bool isEnableOnStart) : base(inputActionMap, isEnableOnStart)
        {
            FireAction = inputActionMap.FindAction("Fire");
            LookAction = inputActionMap.FindAction("Look");
            MoveAction = inputActionMap.FindAction("Move");
        }

        public override void Enable()
        {
            IsEnable = true;
            InputActionMap.Enable();
        }

        public override void Disable()
        {
            IsEnable = false;
            InputActionMap.Disable();
        }
    }
}

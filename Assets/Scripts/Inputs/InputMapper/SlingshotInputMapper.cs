using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public class SlingshotInputMapper : InputMapper
    {
        public readonly InputAction FireAction;
        public readonly InputAction MoveAction;

        public SlingshotInputMapper(InputActionMap inputActionMap, bool isEnableOnStart) : base(inputActionMap, isEnableOnStart)
        {
            FireAction = inputActionMap.FindAction("Fire");
            MoveAction = inputActionMap.FindAction("Move");
        }
    }
}

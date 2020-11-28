using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public class CharacterInputMapper : InputMapper
    {
        public readonly InputAction InteractAction;
        public readonly InputAction MoveAction;

        public CharacterInputMapper(InputActionMap inputActionMap, bool isEnableOnStart) : base(inputActionMap, isEnableOnStart)
        {
            InteractAction = inputActionMap.FindAction("Interact");
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

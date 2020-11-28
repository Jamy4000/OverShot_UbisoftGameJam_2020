using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public class CharacterInputMapper : InputMapper
    {
        public readonly InputAction InteractAction;
        public readonly InputAction MoveAction;
        public readonly InputAction RunAction;
        public readonly InputAction OnPauseAction;

        public CharacterInputMapper(InputActionMap inputActionMap, bool isEnableOnStart) : base(inputActionMap, isEnableOnStart)
        {
            InteractAction = inputActionMap.FindAction("Interact");
            MoveAction = inputActionMap.FindAction("Move");
            RunAction = inputActionMap.FindAction("Run");
            OnPauseAction = inputActionMap.FindAction("Pause");
        }
    }
}

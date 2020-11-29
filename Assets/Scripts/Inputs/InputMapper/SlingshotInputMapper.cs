using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    public class SlingshotInputMapper : InputMapper
    {
        public readonly InputAction FireAction;
        public readonly InputAction MoveAction;
        public readonly InputAction QuitSlingshotModeAction;
        public readonly InputAction OnUpTargetAction;
        public readonly InputAction OnDownTargetAction;
        public readonly InputAction OnPauseAction;

        public SlingshotInputMapper(InputActionMap inputActionMap, bool isEnableOnStart) : base(inputActionMap, isEnableOnStart)
        {
            FireAction = inputActionMap.FindAction("Fire");
            MoveAction = inputActionMap.FindAction("Move");
            QuitSlingshotModeAction = inputActionMap.FindAction("Quit");
            OnPauseAction = inputActionMap.FindAction("Pause");
            OnDownTargetAction = inputActionMap.FindAction("DownTarget");
            OnUpTargetAction = inputActionMap.FindAction("UpTarget");
        }
    }
}

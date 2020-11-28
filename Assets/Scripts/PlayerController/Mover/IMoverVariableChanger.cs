using UnityEngine.InputSystem;

namespace UbiJam.Player
{
    public interface IMoverVariableChanger
    {
        void UpdateMoveVariables(InputAction.CallbackContext obj);
        void UpdateInteractVariables(InputAction.CallbackContext obj);
    }
}

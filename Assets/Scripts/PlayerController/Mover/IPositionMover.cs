using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbiJam.Player
{
    public interface IPositionMover
    {
        void UpdatePosition();

        void UpdateMoveVariables(UnityEngine.InputSystem.InputAction.CallbackContext obj);

        void UpdateInteractVariables(UnityEngine.InputSystem.InputAction.CallbackContext obj);
    }
}

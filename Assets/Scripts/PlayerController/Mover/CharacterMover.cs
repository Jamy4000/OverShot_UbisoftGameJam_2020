using UbiJam.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UbiJam.Player
{
    public class CharacterMover : PlayerMover, IPositionMover
    {
        private bool _isRunning;

        public CharacterMover(CharacterInputMapper actionMapper, Rigidbody rb) : base(rb)
        {
            actionMapper.MoveAction.performed += UpdateMoveVariables;
            actionMapper.MoveAction.canceled += UpdateMoveVariables;

            actionMapper.RunAction.performed += UpdateRunVariable;
            actionMapper.RunAction.canceled += UpdateRunVariable;

            actionMapper.InteractAction.started += UpdateInteractVariables;
            actionMapper.InteractAction.canceled += UpdateInteractVariables;
        }

        public void UpdatePosition()
        {
            if (!IsInteracting)
            {
                Vector3 newPosition = PlayerRigidbody.position + Time.deltaTime * MovementInputValue;
                PlayerRigidbody.MovePosition(newPosition);
            }
        }

        private void UpdateRunVariable(InputAction.CallbackContext obj)
        {
            _isRunning = !obj.canceled;
        }

        protected override void SetCurrentPlayerMoveState(bool isMoving)
        {
            if (!isMoving)
                CurrentPlayerMoveState = EPlayerMoveState.Idle;
            else if (isMoving && _isRunning)
                CurrentPlayerMoveState = EPlayerMoveState.Running;
            else
                CurrentPlayerMoveState = EPlayerMoveState.Walking;
        }
    }
}

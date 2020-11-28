using UbiJam.Inputs;
using UnityEngine;

namespace UbiJam.Player
{
    public class SlingshotMover : PlayerMover, IPositionMover
    {
        private Vector2 _previousInput;

        public SlingshotMover(SlingshotInputMapper actionMapper, Rigidbody rb) : base(rb)
        {
            actionMapper.MoveAction.performed += UpdateMoveVariables;
            actionMapper.FireAction.started += UpdateInteractVariables;
            actionMapper.FireAction.canceled += UpdateInteractVariables;
        }

        public void UpdatePosition()
        {
            if (!IsInteracting)
            {
                if (MovementInputValue == Vector3.zero)
                    return;

                // If user go backward / to the side, use backward curve to go slower and slower
                // If user go forward / toward the center, use forward curve to go faster
                _previousInput = MovementInputValue;
            }
        }

        protected override void SetCurrentPlayerMoveState(bool isMoving)
        {
            if (!isMoving)
                CurrentPlayerMoveState = EPlayerMoveState.Idle;
            else
                CurrentPlayerMoveState = EPlayerMoveState.Walking;
        }
    }
}

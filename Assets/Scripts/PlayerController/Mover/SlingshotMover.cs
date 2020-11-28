using UbiJam.Inputs;
using UnityEngine;

namespace UbiJam.Player
{
    public class SlingshotMover : PlayerMover, IPositionMover
    {
        public SlingshotMover(SlingshotInputMapper actionMapper, Rigidbody rb) : base(rb)
        {
            actionMapper.MoveAction.performed += UpdateMoveVariables;
            actionMapper.FireAction.started += UpdateInteractVariables;
            actionMapper.FireAction.canceled += UpdateInteractVariables;
        }

        public void UpdatePosition()
        {
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

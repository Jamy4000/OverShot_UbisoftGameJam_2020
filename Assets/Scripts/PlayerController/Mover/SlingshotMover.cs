using UbiJam.Inputs;
using UnityEngine;

namespace UbiJam.Player
{
    public class SlingshotMover : PlayerMover, IPositionMover
    {
        private SlingshotSettings _slingshotSettings;
        private Vector3 _slingshotPos;

        public SlingshotMover(SlingshotInputMapper actionMapper, Rigidbody rb) : base(rb)
        {
            actionMapper.MoveAction.performed += UpdateMoveVariables;
            actionMapper.MoveAction.canceled += UpdateMoveVariables;

            actionMapper.FireAction.started += UpdateInteractVariables;
            actionMapper.FireAction.canceled += UpdateInteractVariables;
            _slingshotSettings = GameSettings.Instance.SlingshotSettings;
            _slingshotPos = Slingshot.Slingshot.Instance.transform.position;
        }

        public void UpdatePosition()
        {
            if (!IsInteracting)
            {
                Vector3 newPosition;

                if (MovementInputValue == Vector3.zero)
                {
                    return;
                }
                else
                {
                    float distanceToSling = Vector3.Distance(PlayerRigidbody.position, _slingshotPos);

                    // If user go backward / to the side, use backward curve to go slower and slower
                    if (MovementInputValue.z < 0.0f) //TODO IsGoingTowardTheSide()
                    {
                        newPosition = CalculateNewPosition(_slingshotSettings.SlingshotWalkBackwardCurve, distanceToSling);
                    }
                    // If user go forward / toward the center, use forward curve to go faster
                    else
                    {
                        newPosition = CalculateNewPosition(_slingshotSettings.SlingshotWalkForwardCurve, distanceToSling);
                    }

                    distanceToSling = Vector3.Distance(newPosition, _slingshotPos);

                    if (NewPositionIsValid(distanceToSling))
                        PlayerRigidbody.MovePosition(newPosition);
                }
            }
        }

        private bool NewPositionIsValid(float distanceToSling)
        {
            return distanceToSling < _slingshotSettings.MaxBackwardDistance &&
                distanceToSling > _slingshotSettings.MinForwardDistance;
        }

        private Vector3 CalculateNewPosition(AnimationCurve curve, float distanceToSling)
        {
            distanceToSling /= _slingshotSettings.MaxBackwardDistance;
            return PlayerRigidbody.position + Time.deltaTime * MovementInputValue * curve.Evaluate(distanceToSling);
        }

        protected override void SetCurrentPlayerMoveState(bool isMoving)
        {
            if (!isMoving)
                CurrentPlayerMoveState = EPlayerMoveState.Idle;
            else
                CurrentPlayerMoveState = EPlayerMoveState.Walking;
        }

        /*
         * TODO IF TIME
        private bool IsGoingTowardTheSide(Vector2 newInput)
        {
            // gauche = +1
            // droite = +1
            // if left is negative, slow if is on left side
        }
        */
    }
}

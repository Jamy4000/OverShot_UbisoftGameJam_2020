using UnityEngine;
using UnityEngine.InputSystem;

namespace UbiJam.Player
{
    public abstract class PlayerMover : IMoverVariableChanger
    {
        public EPlayerMoveState CurrentPlayerMoveState { get; protected set; } = EPlayerMoveState.None;

        protected Rigidbody PlayerRigidbody;
        protected bool IsInteracting;
        protected SlingshotSettings SlingshotSettings;
        protected CharacterSettings CharacterSettings;

        protected Vector3 MovementInputValue
        {
            get
            {
                return _movementInputValue * GetPlayerSpeed();
            }
        }

        private Vector3 _movementInputValue; 

        public PlayerMover(Rigidbody rb)
        {
            PlayerRigidbody = rb;
            CharacterSettings = GameSettings.Instance.CharacterSettings;
        }

        public void UpdateMoveVariables(InputAction.CallbackContext obj)
        {
            bool isMoving = !obj.canceled;

            Vector2 readValue = isMoving ? obj.ReadValue<Vector2>() : Vector2.zero;
            SetCurrentPlayerMoveState(isMoving);

            _movementInputValue.x = readValue.x;
            _movementInputValue.z = readValue.y;
        }

        public void UpdateInteractVariables(InputAction.CallbackContext obj)
        {
            IsInteracting = !obj.canceled;
        }

        protected float GetPlayerSpeed()
        {
            switch (CurrentPlayerMoveState) 
            {
                case EPlayerMoveState.Walking:
                    return GetWalkingSpeed();
                case EPlayerMoveState.Running:
                    return CharacterSettings.PlayerRunSpeed;
                default:
                    return 0.0f;
            }
        }

        protected abstract float GetWalkingSpeed();
        protected abstract void SetCurrentPlayerMoveState(bool isMoving);
    }
}
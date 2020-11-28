using UnityEngine;
using UnityEngine.InputSystem;

namespace UbiJam.Player
{
    public abstract class PlayerMover : IMoverVariableChanger
    {
        public EPlayerMoveState CurrentPlayerMoveState { get; protected set; } = EPlayerMoveState.None;

        protected Rigidbody PlayerRigidbody;
        protected Vector3 MovementInputValue;
        protected bool IsInteracting;
        protected CharacterSettings CharacterSettings;

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

            MovementInputValue.x = readValue.x;
            MovementInputValue.z = readValue.y;
            MovementInputValue *= GetPlayerSpeed();
        }

        public void UpdateInteractVariables(InputAction.CallbackContext obj)
        {
            IsInteracting = !obj.canceled;
        }

        protected abstract void SetCurrentPlayerMoveState(bool isMoving);

        protected float GetPlayerSpeed()
        {
            switch (CurrentPlayerMoveState) 
            {
                case EPlayerMoveState.Walking:
                    return CharacterSettings.PlayerWalkSpeed;
                case EPlayerMoveState.Running:
                    return CharacterSettings.PlayerRunSpeed;
                default:
                    return 0.0f;
            }
        }
    }
}
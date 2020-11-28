using UbiJam.Events;
using UbiJam.Gameplay;
using UbiJam.Inputs;
using UbiJam.Utils;
using UnityEngine;

namespace UbiJam.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoSingleton<PlayerController>
    {
        //private PlayerRotator _rotator;

        private IPositionMover _currentMover;
        private SlingshotMover _slingshotMover;
        private CharacterMover _characterMover;

        private bool _canInteract = true;

        protected void Start()
        {
            InputManager inputManager = InputManager.Instance;
            GameSettings gs = GameSettings.Instance;
            Rigidbody rb = GetComponent<Rigidbody>();

            _characterMover = new CharacterMover(inputManager.CharacterInputs, rb);
            _slingshotMover = new SlingshotMover(inputManager.SlingshotInputs, rb);

            _currentMover = _characterMover;
            OnUserSwitchedController.Listeners += BlockUserInteractions;
            OnCharacterReady.Listeners += ActivateCharacterMovements;
            OnSlingshotReady.Listeners += ActivateSlingshotMovements;
        }

        private void Update()
        {
            if (!_canInteract || !GameManager.Instance.IsRunning)
                return;

            _currentMover.UpdatePosition();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnUserSwitchedController.Listeners -= BlockUserInteractions;
            OnCharacterReady.Listeners -= ActivateCharacterMovements;
            OnSlingshotReady.Listeners -= ActivateSlingshotMovements;
        }

        private void BlockUserInteractions(OnUserSwitchedController _)
        {
            _canInteract = false;
        }

        private void ActivateCharacterMovements(OnCharacterReady _)
        {
            _currentMover = _characterMover;
            _canInteract = true;
        }

        private void ActivateSlingshotMovements(OnSlingshotReady _)
        {
            _currentMover = _slingshotMover;
            _canInteract = true;
        }
    }
}
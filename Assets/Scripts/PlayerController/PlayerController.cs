using UbiJam.Events;
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

        private bool _isInSlingshotMode = false;

        protected void Start()
        {
            InputManager inputManager = InputManager.Instance;
            GameSettings gs = GameSettings.Instance;
            Rigidbody rb = GetComponent<Rigidbody>();

            _characterMover = new CharacterMover(inputManager.CharacterInputs, rb);
            _slingshotMover = new SlingshotMover(inputManager.SlingshotInputs, rb);

            _currentMover = _characterMover;
            _isInSlingshotMode = false;
            OnUserSwitchedController.Listeners += SwitchController;
        }

        private void Update()
        {
            _currentMover.UpdatePosition();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnUserSwitchedController.Listeners -= SwitchController;
        }

        private void SwitchController(OnUserSwitchedController info)
        {
            if (_isInSlingshotMode)
                _currentMover = _characterMover;
            else
                _currentMover = _slingshotMover;

            _isInSlingshotMode = !_isInSlingshotMode;
        }
    }
}
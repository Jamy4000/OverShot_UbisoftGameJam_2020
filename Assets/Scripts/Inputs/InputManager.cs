using UnityEngine;
using UbiJam.Utils;
using UbiJam.Events;
using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoSingleton<InputManager>
    {
        private PlayerInput _playerInput;

        public CharacterInputMapper CharacterInputs { get; private set; }
        public SlingshotInputMapper SlingshotInputs { get; private set; }

        private readonly string _slingshotActionMapName = "Slingshot";
        private readonly string _characterActionMapName = "Character";

        protected override void Awake()
        {
            base.Awake();
            _playerInput = GetComponent<PlayerInput>();

            //Setting up slingshot mapper
            _playerInput.SwitchCurrentActionMap(_slingshotActionMapName);
            SlingshotInputs = new SlingshotInputMapper(_playerInput.currentActionMap, false);

            //Setting up character mapper
            _playerInput.SwitchCurrentActionMap(_characterActionMapName);
            CharacterInputs = new CharacterInputMapper(_playerInput.currentActionMap, true);

            OnCharacterReady.Listeners += ToggleCharacterMap;
            OnSlingshotReady.Listeners += ToggleSlingshotMap;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnCharacterReady.Listeners -= ToggleCharacterMap;
            OnSlingshotReady.Listeners -= ToggleSlingshotMap;
        }

        public void ToggleCharacterMap(OnCharacterReady _)
        {
            CharacterInputs.Enable();
            SlingshotInputs.Disable();
            _playerInput.SwitchCurrentActionMap(_characterActionMapName);
        }

        public void ToggleSlingshotMap(OnSlingshotReady _)
        {
            CharacterInputs.Disable();
            SlingshotInputs.Enable();
            _playerInput.SwitchCurrentActionMap(_slingshotActionMapName);
        }
    }
}
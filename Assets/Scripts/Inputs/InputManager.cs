using UnityEngine;
using UbiJam.Utils;
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
        }

        public void ToggleInputMap()
        {
            if (CharacterInputs.IsEnable)
            {
                CharacterInputs.Disable();
                SlingshotInputs.Enable();
            }
            else
            {
                CharacterInputs.Enable();
                SlingshotInputs.Disable();
            }
        }
    }
}
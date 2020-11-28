using UnityEngine;
using UbiJam.Utils;
using UnityEngine.InputSystem;

namespace UbiJam.Inputs
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoSingleton
    {
        private PlayerInput _playerInput;

        public CharacterInputMapper CharacterInputs { get; private set; }
        public SlingshotInputMapper SlingshotInputs { get; private set; }

        private readonly string _slingshotActionMapName = "Slingshot";
        private readonly string _characterActionMapName = "Character";

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            SetupInputMapper(_slingshotActionMapName, false);
            SetupInputMapper(_characterActionMapName, true);
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

        private void SetupInputMapper(string actionMapName, bool isEnableOnStart)
        {
            _playerInput.SwitchCurrentActionMap(actionMapName);

            SlingshotInputs = new SlingshotInputMapper
            (
                _playerInput.currentActionMap,
                isEnableOnStart
            );
        }
    }
}
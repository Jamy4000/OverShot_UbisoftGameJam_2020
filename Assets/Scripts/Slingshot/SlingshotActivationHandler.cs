using UbiJam.Events;
using UbiJam.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UbiJam.Slingshot
{
    public class SlingshotActivationHandler : MonoBehaviour
    {
        private bool _isInSlingshot;

        private void Start()
        {
            InputManager.Instance.SlingshotInputs.QuitSlingshotModeAction.started += QuitSlingshotMode;
            OnUserSwitchedController.Listeners += QuitSlingshotCallback;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isInSlingshot && other.CompareTag(Utils.TagsHolder.PlayerTag))
            {
                _isInSlingshot = true;
                new OnUserSwitchedController(true);
            }
        }

        private void QuitSlingshotCallback(OnUserSwitchedController info)
        {
            _isInSlingshot = info.IsSwitchingToSlingshot;
        }

        private void QuitSlingshotMode(InputAction.CallbackContext _)
        {
            new OnUserSwitchedController(false);
        }
    }
}
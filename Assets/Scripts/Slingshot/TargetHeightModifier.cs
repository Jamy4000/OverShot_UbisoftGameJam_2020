using UbiJam.Events;
using UbiJam.Inputs;
using UnityEngine;

namespace UbiJam.Slingshot
{
    public class TargetHeightModifier : MonoBehaviour
    {
        private float _baseYHeight;
        private bool _isMovingUp;
        private bool _isMovingDown;
        private SlingshotSettings _slingshotSettings;

        private void Awake()
        {
            _baseYHeight = transform.position.y;
            OnCharacterReady.Listeners += DeactivateSystem;
            OnSlingshotReady.Listeners += ActivateSystem;
        }

        private void Start()
        {
            _slingshotSettings = GameSettings.Instance.SlingshotSettings;
            InputManager.Instance.SlingshotInputs.OnUpTargetAction.started += ChangeMoveTargetUpVariable;
            InputManager.Instance.SlingshotInputs.OnUpTargetAction.canceled += ChangeMoveTargetUpVariable;
            InputManager.Instance.SlingshotInputs.OnDownTargetAction.started += ChangeMoveTargetDownVariable;
            InputManager.Instance.SlingshotInputs.OnDownTargetAction.canceled += ChangeMoveTargetDownVariable;
            this.enabled = false;
        }

        private void Update()
        {
            if (_isMovingUp)
            {
                if (_isMovingDown)
                    return;

                MoveTargetUp();
            }
            else if (_isMovingDown)
            {
                MoveTargetDown();
            }
        }

        private void ChangeMoveTargetUpVariable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isMovingUp = !obj.canceled;
        }

        private void ChangeMoveTargetDownVariable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isMovingDown = !obj.canceled;
        }

        private void MoveTargetDown()
        {
            var newPos = transform.position - new Vector3(0.0f, Time.deltaTime * _slingshotSettings.TargetMoveSpeed, 0.0f);
            if (newPos.y >= _baseYHeight)
                transform.position = newPos;
        }

        private void MoveTargetUp()
        {
            var newPos = transform.position + new Vector3(0.0f, Time.deltaTime * _slingshotSettings.TargetMoveSpeed, 0.0f);
            if (newPos.y <= _slingshotSettings.MaxTargetHeight)
                transform.position = newPos;
        }

        private void OnDestroy()
        {
            OnCharacterReady.Listeners -= DeactivateSystem;
            OnSlingshotReady.Listeners -= ActivateSystem;
        }

        private void ActivateSystem(OnSlingshotReady info)
        {
            this.enabled = true;
        }

        private void DeactivateSystem(OnCharacterReady info)
        {
            transform.position = new Vector3(transform.position.x, _baseYHeight, transform.position.z);
            this.enabled = false;
        }
    }
}

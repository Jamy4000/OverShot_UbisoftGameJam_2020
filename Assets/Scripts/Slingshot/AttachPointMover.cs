using UbiJam.Events;
using UbiJam.Inputs;
using UnityEngine;

namespace UbiJam.Slingshot
{
    public class AttachPointMover : MonoBehaviour
    {
        private Transform _attachPoint;
        private Transform _playerCamera;
        private Vector3 _basePosition;
        private bool _isMovingUp;
        private bool _isMovingDown;
        private SlingshotSettings _slingshotSettings;

        private void Awake()
        {
            _attachPoint = transform;
            _basePosition = _attachPoint.position;
            OnCharacterReady.Listeners += DeactivateSystem;
            OnSlingshotReady.Listeners += ActivateSystem;
        }

        private void Start()
        {
            _slingshotSettings = GameSettings.Instance.SlingshotSettings;
            _playerCamera = Camera.main.transform;
            InputManager.Instance.SlingshotInputs.OnUpTargetAction.started += ChangeMoveTargetDownVariable;
            InputManager.Instance.SlingshotInputs.OnUpTargetAction.canceled += ChangeMoveTargetDownVariable;
            InputManager.Instance.SlingshotInputs.OnDownTargetAction.started += ChangeMoveTargetUpVariable;
            InputManager.Instance.SlingshotInputs.OnDownTargetAction.canceled += ChangeMoveTargetUpVariable;
            this.enabled = false;
        }

        private void Update()
        {
            Vector3 newPos = _playerCamera.position + _slingshotSettings.ForwardOffsetAttachPoint;
            newPos.y = _attachPoint.position.y;

            if (_isMovingUp)
            {
                if (_isMovingDown)
                    return;

                MoveTargetUp(ref newPos);
            }
            else if (_isMovingDown)
            {
                MoveTargetDown(ref newPos);
            }

            _attachPoint.position = newPos;
        }

        private void ChangeMoveTargetUpVariable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isMovingUp = !obj.canceled;
        }

        private void ChangeMoveTargetDownVariable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isMovingDown = !obj.canceled;
        }

        private void MoveTargetDown(ref Vector3 newPos)
        {
            Vector3 potentialNewPos = newPos - new Vector3(0.0f, Time.deltaTime * _slingshotSettings.TargetMoveSpeed, 0.0f);
            if (potentialNewPos.y >= _slingshotSettings.MinObjectHeight)
                newPos = potentialNewPos;
        }

        private void MoveTargetUp(ref Vector3 newPos)
        {
            Vector3 potentialNewPos = newPos + new Vector3(0.0f, Time.deltaTime * _slingshotSettings.TargetMoveSpeed, 0.0f);
            if (potentialNewPos.y <= _basePosition.y)
                newPos = potentialNewPos;
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
            transform.position = _basePosition;
            this.enabled = false;
        }
    }
}

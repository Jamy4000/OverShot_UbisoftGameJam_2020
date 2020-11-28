using UnityEngine;
using UbiJam.Events;
using System.Collections;

namespace UbiJam.Player
{
    public class CameraSwitcher : MonoBehaviour
    {
        private Transform _transform;

        private Vector3 _thirdPersonPosition;
        private Quaternion _thirdPersonRotation;

        private GameSettings _gameSettings;
        private SlingshotSettings _slingshotSettings;

        private void Awake()
        {
            OnUserSwitchedController.Listeners += SwitchCameraView;
            _transform = transform;
            _thirdPersonPosition = _transform.localPosition;
            _thirdPersonRotation = _transform.localRotation;
        }

        private void Start()
        {
            _gameSettings = GameSettings.Instance;
            _slingshotSettings = _gameSettings.SlingshotSettings;
        }

        private void OnDestroy()
        {
            OnUserSwitchedController.Listeners -= SwitchCameraView;
        }

        private void SwitchCameraView(OnUserSwitchedController info)
        {
            if (info.IsSwitchingToSlingshot)
                StartCoroutine(SwitchTo1stPerson());
            else
                StartCoroutine(SwitchTo3rdPerson());
        }

        private IEnumerator SwitchTo3rdPerson()
        {
            float interpolationRatio;
            float maxFrameCount = _gameSettings.CharacterSettings.CameraSwitchFrameCount;
            float elapsedFrames = 0;
            float distanceToObjective;

            while (true)
            {
                yield return new WaitForEndOfFrame();

                interpolationRatio = elapsedFrames / maxFrameCount;

                _transform.localPosition = Vector3.Lerp(_transform.localPosition, _thirdPersonPosition, interpolationRatio);
                _transform.localRotation = Quaternion.Lerp(_transform.localRotation, _thirdPersonRotation, interpolationRatio);

                distanceToObjective = Vector3.Distance(_transform.localPosition, _thirdPersonPosition);
                // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
                elapsedFrames++;
                if (elapsedFrames >= maxFrameCount)
                    break;
                else if (distanceToObjective < 2.0f)
                {
                    _transform.localPosition = _thirdPersonPosition;
                    _transform.localRotation = _thirdPersonRotation;
                    break;
                }
            }

            new OnCharacterReady();
        }

        private IEnumerator SwitchTo1stPerson()
        {
            float interpolationRatio;
            float maxFrameCount = _gameSettings.SlingshotSettings.CameraSwitchFrameCount;
            float elapsedFrames = 0.0f;
            float distanceToObjective;

            while (true)
            {
                yield return new WaitForEndOfFrame();

                interpolationRatio = elapsedFrames / maxFrameCount;
                _transform.localPosition = Vector3.Lerp(_transform.localPosition, _slingshotSettings.CameraTargetPosition, interpolationRatio);
                _transform.localRotation = Quaternion.Lerp(_transform.localRotation, Quaternion.identity, interpolationRatio);

                distanceToObjective = Vector3.Distance(_transform.localPosition, _slingshotSettings.CameraTargetPosition);

                elapsedFrames++;
                if (elapsedFrames == maxFrameCount)
                    break;
                else if (distanceToObjective < 0.1f)
                {
                    _transform.localPosition = _slingshotSettings.CameraTargetPosition;
                    _transform.localRotation = Quaternion.identity;
                    break;
                }
            }

            new OnSlingshotReady();
        }
    }
}

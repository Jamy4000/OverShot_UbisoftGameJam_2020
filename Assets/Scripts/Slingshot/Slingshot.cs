using UbiJam.Inputs;
using UbiJam.Utils;
using UbiJam.Events;
using UbiJam.GrabbableObjects;
using UnityEngine;
using UbiJam.Player;
using System;

namespace UbiJam.Slingshot
{
    public class Slingshot : MonoSingleton<Slingshot>
	{
        [SerializeField]
        private Transform _attachPoint;
        [SerializeField]
        private Transform _throwingPoint;
        [SerializeField]
        private GrabbablePool _slingshotPool;
        [SerializeField]
        private Vector3 _forwardOffsetAttachPoint = new Vector3(0.0f, 0.0f, 0.2f);

        public GameObject ObjectInSlingshot { get; private set; }
        private Transform _playerCamera;
        private bool _isActive;

        protected override void Awake()
        {
            base.Awake();
            OnSlingshotReady.Listeners += ActivateSlingshot;
            OnCharacterReady.Listeners += DeactivateSlingshot;
            _playerCamera = Camera.main.transform;
        }

        private void Update()
        {
            if (_isActive)
            {
                _attachPoint.position = _playerCamera.position + _forwardOffsetAttachPoint;
            }
        }

        private void Start()
        {
            InputManager.Instance.SlingshotInputs.FireAction.started += ReleaseSlingshot;
            InputManager.Instance.SlingshotInputs.QuitSlingshotModeAction.started += RemoveGrabbable;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnSlingshotReady.Listeners -= ActivateSlingshot;
            OnCharacterReady.Listeners -= DeactivateSlingshot;
		}

        private void ActivateSlingshot(OnSlingshotReady info)
        {
            for (int i = 0; i < _slingshotPool.GrabbableObjectReferences.Count; i++)
            {
                if (_slingshotPool.GrabbableObjectReferences[i].Type == ObjectGrabber.Instance.CurrentlyGrabbedObject)
                {
                    ObjectInSlingshot = _slingshotPool.GrabbableObjectReferences[i].SceneGO;
                    ObjectInSlingshot.SetActive(true);
                    _isActive = true;
                }
            }
        }

        private void DeactivateSlingshot(OnCharacterReady info)
        {
            _isActive = false;
            ObjectInSlingshot.SetActive(false);
        }

        private void RemoveGrabbable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isActive = false;
            //Do anim for _attachPoint
            ObjectInSlingshot.SetActive(false);
        }

        private void ReleaseSlingshot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Rigidbody rigidbodyInSlingshot = ObjectInSlingshot.GetComponent<Rigidbody>();
            rigidbodyInSlingshot.isKinematic = false;
            rigidbodyInSlingshot.AddForce(GetThrowForce(), ForceMode.Impulse);

            //Do anim for _attachPoint
        }

        private Vector3 GetThrowForce()
        {
            SlingshotSettings slingshotSettings = GameSettings.Instance.SlingshotSettings;
            float distanceFromThrowingPoint = Vector3.Distance(_throwingPoint.position, _playerCamera.position);
            distanceFromThrowingPoint /= slingshotSettings.MaxBackwardDistance;
            Vector3 direction = _throwingPoint.position - _attachPoint.position;
            direction += Physics.gravity * (1-distanceFromThrowingPoint);
            return direction * slingshotSettings.ThrowForce;
        }
    }
}

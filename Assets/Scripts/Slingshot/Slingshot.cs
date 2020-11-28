using UbiJam.Inputs;
using UbiJam.Utils;
using UbiJam.Events;
using UbiJam.GrabbableObjects;
using UnityEngine;
using UbiJam.Player;
using System.Collections;

namespace UbiJam.Slingshot
{
    public class Slingshot : MonoSingleton<Slingshot>
    {
        [SerializeField]
        private Transform _attachPoint;
        [SerializeField]
        private StretchToPoint[] _stretchers;
        [SerializeField]
        private SlingshotActivationHandler _activationHandler;
        [SerializeField]
        private Transform _throwingPoint;
        [SerializeField]
        private GrabbablePool _slingshotPool;

        public GameObject ObjectInSlingshot { get; private set; }
        private Transform _playerCamera;
        private bool _isActive;
        private Vector3 _attachPointBasePos;
        SlingshotSettings _slingshotSettings;

        protected override void Awake()
        {
            base.Awake();
            OnSlingshotReady.Listeners += ActivateSlingshot;
            OnCharacterReady.Listeners += DeactivateSlingshot;
            _playerCamera = Camera.main.transform;
            _attachPointBasePos = _attachPoint.localPosition;
        }

        private void Update()
        {
            if (_isActive)
            {
                _attachPoint.position = _playerCamera.position + _slingshotSettings.ForwardOffsetAttachPoint;
            }
        }

        private void Start()
        {
            _slingshotSettings = GameSettings.Instance.SlingshotSettings;
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
            //TODO Do anim for _attachPoint
            ObjectInSlingshot.SetActive(false);
        }

        private void ReleaseSlingshot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Rigidbody rigidbodyInSlingshot = ObjectInSlingshot.GetComponent<Rigidbody>();
            rigidbodyInSlingshot.isKinematic = false;
            rigidbodyInSlingshot.AddForce(GetThrowForce(), ForceMode.Impulse);

            foreach (var stretcher in _stretchers)
            {
                stretcher.ResetPostion();
            }
            
            _attachPoint.localPosition = _attachPointBasePos;
            _isActive = false;


            StartCoroutine(LeaveSlingshot());
            //TODO Do anim for _attachPoint
        }

        private IEnumerator LeaveSlingshot()
        {
            _activationHandler.CanLeave = false;
            yield return new WaitForSeconds(1.0f);
            ObjectInSlingshot.GetComponent<GrabbableObject>().ResetGrabbable();
            new OnUserSwitchedController(false);
            _activationHandler.CanLeave = true;
        }

        private Vector3 GetThrowForce()
        {
            float distanceFromThrowingPoint = Vector3.Distance(_throwingPoint.position, _playerCamera.position);
            distanceFromThrowingPoint /= _slingshotSettings.MaxBackwardDistance;
            Vector3 direction = _throwingPoint.position - _attachPoint.position;
            direction *= distanceFromThrowingPoint;
            direction += Physics.gravity * _slingshotSettings.GravityMultiplier * (1 - distanceFromThrowingPoint);
            return direction * _slingshotSettings.ThrowForce;
        }
    }
}

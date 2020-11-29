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
        private StretchToPoint[] _stretchers;
        [SerializeField]
        private SlingshotActivationHandler _activationHandler;
        [SerializeField]
        private Transform _throwingPoint;
        [SerializeField]
        private GrabbablePool _slingshotPool;
        [SerializeField]
        private LineRenderer _slingshotRenderer;

        public GameObject ObjectInSlingshot { get; private set; }
        private Transform _playerCamera;
        SlingshotSettings _slingshotSettings;

        public Vector3[] Points;

        public bool HasThrown;

        protected override void Awake()
        {
            base.Awake();
            OnSlingshotReady.Listeners += ActivateSlingshot;
            OnCharacterReady.Listeners += DeactivateSlingshot;
            _playerCamera = Camera.main.transform;
            Points = new Vector3[_slingshotRenderer.positionCount];
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
                }
            }
        }

        private void DeactivateSlingshot(OnCharacterReady info)
        {
            ObjectInSlingshot.SetActive(false);
        }

        private void RemoveGrabbable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            //TODO Do anim for _attachPoint
            ObjectInSlingshot.SetActive(false);
        }

        private void ReleaseSlingshot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            //Rigidbody rigidbodyInSlingshot = ObjectInSlingshot.GetComponent<Rigidbody>();
            //rigidbodyInSlingshot.isKinematic = false;
            //rigidbodyInSlingshot.AddForce(GetThrowForce(), ForceMode.Impulse);
            HasThrown = true;
            ObjectInSlingshot.GetComponent<ThrowableObject>().ThrowThisObject(Points);

            foreach (var stretcher in _stretchers)
            {
                stretcher.ResetPostion();
            }

            StartCoroutine(LeaveSlingshot());
            //TODO Do anim for _attachPoint
        }

        private IEnumerator LeaveSlingshot()
        {
            _activationHandler.CanLeave = false;
            yield return new WaitForSeconds(1.0f);
            new OnUserSwitchedController(false);
            _activationHandler.CanLeave = true;
            yield return new WaitForSeconds(3.0f);

            HasThrown = false;
            ObjectInSlingshot.GetComponent<GrabbableObject>().ResetGrabbable();
        }

        public Vector3 GetThrowForce()
        {
            float distanceFromThrowingPoint = Vector3.Distance(_throwingPoint.position, _playerCamera.position);
            distanceFromThrowingPoint /= _slingshotSettings.MaxBackwardDistance;

            Vector3 direction = _throwingPoint.position - ObjectInSlingshot.transform.position;
            direction *= distanceFromThrowingPoint;

            return direction * _slingshotSettings.ThrowForce;
        }
    }
}

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
        private Rigidbody _attachPointRb;
        [SerializeField]
        private GrabbablePool _slingshotPool;

        public GameObject ObjectInSlingshot { get; private set; }

		protected override void Awake()
        {
            base.Awake();
            OnSlingshotReady.Listeners += ActivateSlingshot;
            OnCharacterReady.Listeners += DeactivateSlingshot;
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
                }
            }
        }

        private void DeactivateSlingshot(OnCharacterReady info)
        {
            ObjectInSlingshot.SetActive(false);
        }

        private void RemoveGrabbable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            ObjectInSlingshot.SetActive(false);
        }

        private void ReleaseSlingshot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Rigidbody rigidbodyInSlingshot = ObjectInSlingshot.GetComponent<Rigidbody>();
            Vector3 throwForce = SlingshotCurveCalculator.GetForce();
            _attachPointRb.AddForce(throwForce, ForceMode.Acceleration);
            rigidbodyInSlingshot.AddForce(throwForce, ForceMode.Acceleration);
        }
	}
}

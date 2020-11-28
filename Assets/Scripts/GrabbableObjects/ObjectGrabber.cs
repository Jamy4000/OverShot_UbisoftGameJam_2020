﻿using System;
using System.Collections.Generic;
using UbiJam.Events;
using UbiJam.GrabbableObjects;
using UbiJam.Inputs;
using UbiJam.Utils;
using UnityEngine;

namespace UbiJam.Player
{
    public class ObjectGrabber : MonoSingleton<ObjectGrabber>
    {
        [SerializeField]
        private GrabbablePool _playerPool;

        private GrabbableObjectType _closestGrabbable = null;
        private Transform _transform;
        private List<GrabbableObjectType> _sceneObjects;
        private GameSettings _settings;

        public EGrabbableObjects CurrentlyGrabbedObject { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
            OnGameStarted.Listeners += ActivateSystem;
            OnCharacterReady.Listeners += ActivateSystem;
            OnUserSwitchedController.Listeners += DeactivateSystem;
            OnGameEnded.Listeners += DeactivateSystem;
        }

        private void Start()
        {
            _sceneObjects = GrabbableSpawner.Instance.Pool.GrabbableObjectReferences;
            _settings = GameSettings.Instance;

            InputManager inputManager = InputManager.Instance;
            inputManager.SlingshotInputs.OnPauseAction.started += DisableSystem;
            inputManager.CharacterInputs.OnPauseAction.started += DisableSystem;

            inputManager.SlingshotInputs.FireAction.started += ReleaseSlingshot;
            inputManager.CharacterInputs.InteractAction.started += ReleaseOrGrab;
        }

        private void Update()
        {
            if (CurrentlyGrabbedObject == EGrabbableObjects.None)
                CheckClosestObject();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnGameStarted.Listeners -= ActivateSystem;
            OnGameEnded.Listeners -= DeactivateSystem;
            OnCharacterReady.Listeners -= ActivateSystem;
            OnUserSwitchedController.Listeners -= DeactivateSystem;
        }

        private void CheckClosestObject()
        {
            Vector3 currentPos = _transform.position;
            GrabbableObjectType bestDistanceGrabbable = null;
            float calculatedDistance;
            float bestDistance = float.PositiveInfinity;

            for (int i = 0; i < _sceneObjects.Count; i++)
            {
                if (_sceneObjects[i].Type == EGrabbableObjects.None)
                    continue;

                calculatedDistance = Vector3.Distance(_sceneObjects[i].SceneGO.transform.position, currentPos);
                if (calculatedDistance < _settings.MaxGrabDistance && calculatedDistance < bestDistance)
                {
                    bestDistance = calculatedDistance;
                    bestDistanceGrabbable = _sceneObjects[i];
                }
            }

            _closestGrabbable = bestDistanceGrabbable;
        }

        private void ReleaseOrGrab(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            // User can grab
            if (_closestGrabbable != null && _closestGrabbable.Type != EGrabbableObjects.None && CurrentlyGrabbedObject == EGrabbableObjects.None)
            {
                CurrentlyGrabbedObject = _closestGrabbable.Type;
                ChangePlayerObjectActivation(true);
                _closestGrabbable = null;
            }
            // We try to release
            else 
            {
                ReleaseObject();
                CurrentlyGrabbedObject = EGrabbableObjects.None;
            }
        }

        private void ReleaseObject()
        {
            if (CurrentlyGrabbedObject == EGrabbableObjects.None)
                return;

            ChangePlayerObjectActivation(false);
        }

        private void ChangePlayerObjectActivation(bool newStatus)
        {
            for (int i = 0; i < _playerPool.GrabbableObjectReferences.Count; i++)
            {
                if (_playerPool.GrabbableObjectReferences[i].Type != CurrentlyGrabbedObject)
                    continue;

                _playerPool.GrabbableObjectReferences[i].SceneGO.SetActive(newStatus);
                return;
            }
        }

        private void ActivateSystem(OnGameStarted info)
        {
            enabled = true;
        }

        private void ActivateSystem(OnCharacterReady info)
        {
            enabled = true;
            if (CurrentlyGrabbedObject != EGrabbableObjects.None)
                ChangePlayerObjectActivation(true);
        }

        private void DeactivateSystem(OnGameEnded info)
        {
            enabled = false;
        }

        private void DeactivateSystem(OnUserSwitchedController info)
        {
            if (info.IsSwitchingToSlingshot)
            {
                enabled = false;
                ReleaseObject();
            }
        }

        private void DisableSystem(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            enabled = false;
        }

        private void ReleaseSlingshot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            CurrentlyGrabbedObject = EGrabbableObjects.None;
        }
    }
}

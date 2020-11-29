using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UbiJam.Events;
using System;
using UbiJam.Inputs;
using UbiJam.Player;

namespace UbiJam.SFX
{
    public class SFXPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _itemGrabSfx;
        [SerializeField]
        private AudioClip _slingshotFireSfx;
        [SerializeField]
        private AudioClip _slingshotHitSfx;
        [SerializeField]
        private AudioClip _slingshotMissSfx;
        [SerializeField]
        private AudioClip _slingshotLoadedSfx;

        private AudioSource _source;
        private PlayerController _playerController;
        
        private void Awake()
        {
            OnObjectWasGrabbed.Listeners += PlayGrabEffect;
            OnUserSwitchedController.Listeners += PlaySlingShotLoad;
            OnSlingshotHit.Listeners += PlaySlingshotHit;
            OnSlingshotMiss.Listeners += PlaySlingshotMiss;
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _playerController = PlayerController.Instance;
            InputManager.Instance.SlingshotInputs.FireAction.started += PlaySlingShotEffect;
        }

        //private void Update()
        //{
        //    switch  (_playerController.CurrentMoveState)
        //    {
        //        case EPlayerMoveState.Walking:

        //            break;
        //        case EPlayerMoveState.Running:

        //            break;
        //        case EPlayerMoveState.Idle:

        //            break;
        //    }
        //}

        private void OnDestroy()
        {
            OnObjectWasGrabbed.Listeners -= PlayGrabEffect;
            OnUserSwitchedController.Listeners -= PlaySlingShotLoad;
            OnSlingshotHit.Listeners -= PlaySlingshotHit;
            OnSlingshotMiss.Listeners -= PlaySlingshotMiss;
        }

        private void PlayGrabEffect(OnObjectWasGrabbed _)
        {
            _source.clip = _itemGrabSfx[UnityEngine.Random.Range(0, _itemGrabSfx.Length - 1)];
            _source.Play();
        }

        private void PlaySlingShotEffect(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _source.clip = _slingshotFireSfx;
            _source.Play();
        }

        private void PlaySlingShotLoad(OnUserSwitchedController info)
        {
            if (!info.IsSwitchingToSlingshot)
                return;

            _source.clip = _slingshotLoadedSfx;
            _source.Play();
        }

        private void PlaySlingshotMiss(OnSlingshotMiss info)
        {
            _source.clip = _slingshotMissSfx;
            _source.Play();
        }

        private void PlaySlingshotHit(OnSlingshotHit info)
        {
            _source.clip = _slingshotHitSfx;
            _source.Play();
        }
    }
}
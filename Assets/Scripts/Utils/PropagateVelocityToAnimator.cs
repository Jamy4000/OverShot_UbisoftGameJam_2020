using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UbiJam.Inputs;
using UbiJam.Gameplay;
using System;

namespace UbiJam.Player
{
    public class PropagateVelocityToAnimator : MonoBehaviour
    {
        public Animator anim;
        public float rotationMultiplier = 1f;
        public float runMultiplier = 2f;
        public float walkMultiplier = 1f;
        private Vector2 readValue;
        private bool _isClicking;

        // Update is called once per frame

        private void Start()
        {
            InputManager inputManager = InputManager.Instance;
            inputManager.CharacterInputs.MoveAction.performed += UpdateMoveVariables;
            inputManager.CharacterInputs.MoveAction.canceled += UpdateMoveVariables;

            inputManager.SlingshotInputs.MoveAction.performed += UpdateMoveVariables;
            inputManager.SlingshotInputs.MoveAction.canceled += UpdateMoveVariables;
        }

        private void Update()
        {
            if (_isClicking)
            {
                RotateCharacter();
            }
        }

        private void UpdateMoveVariables(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isClicking = !obj.canceled;

            if (!GameManager.Instance.IsRunning)
                return;

            // readValue.x < 0 = going left, readValue.x > 0 = going right 
            // readValue.y < 0 = going backward, readValue.y > 0 = going forward
            readValue = _isClicking ? obj.ReadValue<Vector2>() : Vector2.zero;
            anim.SetFloat("Speed",  readValue.magnitude * walkMultiplier);
        }

        private void RotateCharacter()
        {
            if (readValue.x > 0)
                anim.transform.rotation = Quaternion.Slerp(anim.transform.rotation, Quaternion.LookRotation(Vector3.right), rotationMultiplier * Time.deltaTime);
            else if (readValue.x < 0)
                anim.transform.rotation = Quaternion.Slerp(anim.transform.rotation, Quaternion.LookRotation(Vector3.left), rotationMultiplier * Time.deltaTime);

            if (readValue.y > 0)
                anim.transform.rotation = Quaternion.Slerp(anim.transform.rotation, Quaternion.LookRotation(Vector3.forward), rotationMultiplier * Time.deltaTime);
            else if (readValue.y < 0)
                anim.transform.rotation = Quaternion.Slerp(anim.transform.rotation, Quaternion.LookRotation(Vector3.back), rotationMultiplier * Time.deltaTime);
        }

        private void UpdateRunVariable(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isClicking = !obj.canceled;

            if (!GameManager.Instance.IsRunning)
                return;

            // readValue.x < 0 = going left, readValue.x > 0 = going right 
            // readValue.y < 0 = going backward, readValue.y > 0 = going forward
            readValue = _isClicking ? obj.ReadValue<Vector2>() : Vector2.zero;
            anim.SetFloat("Speed",  readValue.magnitude * runMultiplier);
        }
    }
}

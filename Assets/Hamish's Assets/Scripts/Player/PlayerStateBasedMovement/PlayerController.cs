using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

namespace Hamish.player
{
    public class PlayerController : StateController
    {
        private Rigidbody rb;

        [SerializeField] private float sensitivity;

        private void Awake()
        {
            StartState(new Walking(this));
            rb = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            RunStateMachine();
            ManipulateRigidBody();
            MovePlayer();
        }

        #region CameraMovement
        [Header("Camera")]
        [SerializeField] private GameObject cameraRig;
        [SerializeField] private GameObject cameraPosition;
        private float XRotation;
        private float YRotation;

        public void ControlCamera(float mouseY, float mouseX)
        {
            cameraRig.transform.position = cameraPosition.transform.position;

            //NOTE: This looks fucking stupid and that's cus unity is wierd. It works so idgaf

            XRotation -= mouseY;
            YRotation += mouseX;

            XRotation = Mathf.Clamp(XRotation, -90f, 90f);

            cameraRig.transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
            transform.rotation = Quaternion.Euler(0, YRotation, 0);
        }
        #endregion

        #region MovePlayer
        private Vector3 movementDirection;
        public void SetMovementDirection(float X, float Y)
        {
            movementDirection = transform.forward * Y + transform.right * X;
        }

        public void MovePlayer()
        {
            rb.AddForce(currentSpeed * movementDirection.normalized, ForceMode.Force);
        }
        #endregion

        [Header("Variables")]
        [Range(0f, 50)][SerializeField] private float groundSpeed;
        [Range(0f, 50)][SerializeField] private float runningSpeed;
        [Range(0f, 50)][SerializeField] private float groundDrag;
        [Range(0f, 50)][SerializeField] private float airSpeed;
        [Range(0f, 50)][SerializeField] private float airDrag;
        private float currentSpeed;

        private void ManipulateRigidBody()
        {
            switch (currentMove)
            {
                case Walking:
                    rb.drag = groundDrag;
                    currentSpeed = groundSpeed;
                    break;
                case Running:
                    rb.drag = groundDrag;
                    currentSpeed = runningSpeed;
                    break;
            }
        }

        public float GetSensitivity()
        {
            return sensitivity;
        }

    }
}
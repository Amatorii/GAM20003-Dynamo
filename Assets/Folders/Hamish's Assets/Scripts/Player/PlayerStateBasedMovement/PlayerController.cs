using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
            //Debug.Log(grounded);
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

        public void Jump()
        {
            rb.AddForce(15 * jumpForce * transform.up, ForceMode.Force);
        }
        #endregion

        #region Collision
        [Header("Collision")]
        [SerializeField] private GameObject feet;
        public bool grounded { get; private set; }
        //public bool grounded;
        public void RunCollisionChecks(float detectionRayLength)
        {
            //Debug.DrawRay(feet.transform.position, transform.TransformDirection(Vector3.down), Color.red, detectionRayLength);
            if (Physics.Raycast(feet.transform.position, transform.TransformDirection(Vector3.down), out RaycastHit _hit, detectionRayLength))
            {
                grounded = true;
            }
            else
                grounded = false;
        }

        public void SnapPlayerToGround(float detectionRayLength)
        {
            Physics.Raycast(feet.transform.position, transform.TransformDirection(Vector3.down), out RaycastHit _hit, detectionRayLength);
            if (_hit.point != transform.position)
            {
                transform.position = _hit.point;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Rail":
                    SetState(new Grinding(this));
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (currentMove)
            {
                case Grinding:
                    SetState(new Walking(this));
                    break;
            }
        }
        #endregion

        [Header("Variables")]
        [Range(0f, 50)][SerializeField] private float groundSpeed;
        [Range(0f, 50)][SerializeField] private float runningSpeed;
        [Range(0f, 50)][SerializeField] private float groundDrag;
        [Range(0f, 50)][SerializeField] private float airSpeed;
        [Range(0f, 50)][SerializeField] private float airDrag;
        [Range(0f, 50)][SerializeField] private float jumpForce;
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
                case Airborne:
                    rb.drag = airDrag;
                    currentSpeed = airSpeed;
                    break;
            }
        }

        public float GetSensitivity()
        {
            return sensitivity;
        }

    }
}
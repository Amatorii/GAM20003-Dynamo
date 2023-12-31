using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Hamish.player.draft.PCDocumentation;

namespace Hamish.player.draft
{
    /// <summary>
    /// The main script that controls the player's movement
    /// </summary>
    public class PlayerDraftControlerCopy : PCDocumentation, IPlayerController
    {
        [Range(0f, 10f)]
        [SerializeField] private float sensitivity;

        private Rigidbody rb;
        public Transform orientation { get; private set; }

        private FrameInput Input;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            orientation = feet.transform;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Drag();
            ControlCamera();
            GatherInput();
            RunCollisionChecks();
            MovePlayer();
        }

        private void FixedUpdate()
        {

        }

        #region GatherInput
        public override void GatherInput()
        {
            Input = new FrameInput()
            {
                JumpDown = UnityEngine.Input.GetButtonDown("Jump"),
                JumpUp = UnityEngine.Input.GetButtonUp("Jump"),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Y = UnityEngine.Input.GetAxisRaw("Vertical"),
                mouseX = UnityEngine.Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity * 40f,
                mouseY = UnityEngine.Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity * 40f
            };
        }
        #endregion

        #region CameraMovement

        [SerializeField] private GameObject cameraRig;
        private float XRotation;
        private float YRotation;

        private void ControlCamera()
        {
            cameraRig.transform.position = transform.position + new Vector3(0, 1, 0);

            //NOTE: This looks fucking stupid and that's cus unity is wierd. It works so idgaf

            XRotation -= Input.mouseY;
            YRotation += Input.mouseX;

            if (grounded)
            {
                XRotation = Mathf.Clamp(XRotation, -90f, 90f);
            }


            cameraRig.transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
            orientation.rotation = Quaternion.Euler(0, YRotation, 0);
        }

        #endregion

        #region Collision
        [Header("Collision")]
        [SerializeField] private float detectionRayLength;
        [SerializeField] private GameObject feet;
        public bool grounded { get; private set; }

        private void RunCollisionChecks()
        {
            RaycastHit _hit;

            if (Physics.Raycast(feet.transform.position, transform.TransformDirection(Vector3.down), out _hit, detectionRayLength))
                grounded = true;
            else
                grounded = false;
        }
        #endregion

        #region GroundedMovement
        [Header("Grounded")]
        [Range(0f, 50)]
        [SerializeField] private float groundedSpeed;
        [Range(0f, 50)]
        [SerializeField] private float airSpeed;
        [Range(0f, 50)]
        [SerializeField] private float jumpForce;
        private Vector3 movementDirection;
        private void MovePlayer()
        {
            movementDirection = orientation.forward * Input.Y + orientation.right * Input.X;

            if (Input.JumpDown & grounded)
            {
                rb.AddForce(10.0f * jumpForce * orientation.up.normalized, ForceMode.Force);
            }

            if (grounded)
            {
                rb.AddForce(10.0f * groundedSpeed * movementDirection.normalized, ForceMode.Force);
            }
            else
                rb.AddForce(airSpeed * movementDirection.normalized, ForceMode.Force);
        }

        #endregion

        #region Physics
        [Header("Physics Sliders")]
        ///Currently not really used but there is potential
        ///If you want things like clamped fall speed, apex jump modifiers, cyote time, here is where I would put them
        public TextMeshProUGUI velocityText;
        [SerializeField] private float groundDrag;
        private void Drag()
        {
            if (!grounded)
                rb.drag = 0f;
            else
                rb.drag = groundDrag;
            velocityText.text = rb.velocity.ToString(); //Calculates player velocity 
        }

        #endregion
    }
}
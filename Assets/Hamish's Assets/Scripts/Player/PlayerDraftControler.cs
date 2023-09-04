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
    public class PlayerDraftControler : PCDocumentation, IPlayerController
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
            if (Input.JumpDown & grounded)
            {
                Jump();
            }
            movementDirection = orientation.forward * Input.Y + orientation.right * Input.X;
        }

        private void FixedUpdate()
        {
            if (Input.Shift)
                MovePlayer(sprintSpeed * 10);
            else
                MovePlayer(groundedSpeed * 10);
        }

        #region GatherInput
        public override void GatherInput()
        {
            Input = new FrameInput()
            {
                JumpDown = UnityEngine.Input.GetButtonDown("Jump"),
                JumpUp = UnityEngine.Input.GetButtonUp("Jump"),
                Shift = UnityEngine.Input.GetKey("left shift"),
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

            XRotation = Mathf.Clamp(XRotation, -90f, 90f);

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
        [SerializeField] private float sprintSpeed;
        [Range(0f, 50)]
        [SerializeField] private float jumpForce;
        [Range(0f, 50)]
        [SerializeField] private float downForce;
        private Vector3 movementDirection;
        private void MovePlayer(float speed)
        {

            if (grounded)
            {
                rb.AddForce(speed * movementDirection.normalized, ForceMode.Force);
            }
            else
            {
                rb.AddForce(airSpeed * movementDirection.normalized, ForceMode.Force);
            }
        }

        private void Jump()
        {
                rb.AddForce(10 * jumpForce * orientation.up.normalized, ForceMode.Force);
        }

        #endregion

        #region Physics
        [Header("Physics Sliders")]
        ///Currently not really used but there is potential
        ///If you want things like clamped fall speed, apex jump modifiers, cyote time, here is where I would put them
        //public TextMeshProUGUI velocityText;
        /// <summary>
        /// Decrease this to make the ground more slipery 
        /// </summary>
        [SerializeField] private float groundDrag;
        /// <summary>
        /// Decrrease this to make air movement slower
        /// </summary>
        [SerializeField] private float airDrag;
        private void Drag()
        {
            if (!grounded)
                rb.drag = 0f;
            else
                rb.drag = groundDrag;
            //velocityText.text = rb.velocity.ToString(); //Calculates player velocity 
            //velocityText.text += grounded.ToString(); //Calculates player velocity 
        }

        #endregion
    }
}
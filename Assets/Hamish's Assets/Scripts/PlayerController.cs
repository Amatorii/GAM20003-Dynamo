using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hamish.player
{
    /// <summary>
    /// The main script that controls the player's movement
    /// </summary>
    public class PlayerController : MonoBehaviour
    {       
        [Range(0f, 10f)]
        [SerializeField]private float sensitivity;

        private Rigidbody rb;
        [SerializeField] private Transform orientation;

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
            Gravity();
            ControlCamera();
            RunCollisionChecks();
            GatherInput();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        #region GatherInput
        private struct FrameInput
        {
            /// <summary>
            /// This only refers to the directions on a keyboard (A,D) and doesn't refer to movement
            /// </summary>
            public float X, Y;
            public float mouseX, mouseY;
            public bool JumpDown;
            public bool JumpUp;
        }

        /// <summary>
        /// Gets the player's input from their keyboard. Not currently customizable ATM
        /// </summary>
        private void GatherInput()
        {
            Input = new FrameInput()
            {
                JumpDown = UnityEngine.Input.GetButtonDown("Jump"),
                JumpUp = UnityEngine.Input.GetButtonUp("Jump"),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Y = UnityEngine.Input.GetAxisRaw("Vertical"),
                mouseX = UnityEngine.Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity * 20f,
                mouseY = UnityEngine.Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity * 20f
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
        [SerializeField] private bool grounded = false;
        [SerializeField] private GameObject feet;
        [SerializeField] private float detectionRayLength;
        /// <summary>
        /// Does what it says on the tin
        /// </summary>
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
        [Range(0f, 300f)]
        [SerializeField] private float Speed;
        [SerializeField] private float groundDrag;
        private Vector3 movementDirection;
        private void MovePlayer()
        {
            movementDirection = orientation.forward * Input.Y + orientation.right * Input.X;

            rb.AddForce(movementDirection.normalized * Speed * 10f, ForceMode.Force);
        }

        #endregion

        #region Gravity
        //[Header("Gravity")]

        //[SerializeField] private float fallSpeedClamp;
        //private float fallSpeed;
        public TextMeshProUGUI text;
        private void Gravity()
        {
            ///Currently not really used but there is potential
            ///If you want things like clamped fall speed, apex jump modifiers, cyote time, here is where I would put them
            if (!grounded)
                rb.drag = 0f;
            else 
                rb.drag = groundDrag;
            //    rb.AddForce(Vector3.down, ForceMode.Acceleration);
            text.text = rb.velocity.ToString(); //Calculates player velocity 
        }

        #endregion
    }
}
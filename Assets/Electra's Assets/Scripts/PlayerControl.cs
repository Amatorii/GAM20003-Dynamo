using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody move;

    private Vector3 movInputs;
    private Vector3 lookInputs;
    public Vector3 movePos;

    private Quaternion headRot;

    private CapsuleCollider crouching;

    public Transform head;
    public GameObject projectile;

    public float sinceLastGrounded = 0;
    public float moveSpeed = 10f;
    public float jumpHeight = 5f;
    public float gravity = 15f;

    void Awake()
    {
        move = GetComponent<Rigidbody>();
        crouching = GetComponent<CapsuleCollider>();
    }
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pooling inputs
        movInputs.x = Input.GetAxis("Horizontal");
        movInputs.z = Input.GetAxis("Vertical");

        lookInputs.y += Input.GetAxis("Mouse X");
        lookInputs.x -= Input.GetAxis("Mouse Y");

        //detects input for jump, runs jump function
        if (Input.GetButtonDown("Jump") && sinceLastGrounded < 0.1f)
            Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching.height = 1f;
            moveSpeed = 5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouching.height = 2f;
            moveSpeed = 10f;
        }

        Vector3 newDir = Vector3.zero;

        newDir = head.transform.TransformDirection(movInputs);


        movePos = new Vector3(newDir.x * moveSpeed, movePos.y, newDir.z * moveSpeed);

        //applying gravity
        if (sinceLastGrounded <= 0.1f)
        {
            movePos.y -= 0.2f * Time.deltaTime;
            movePos.y = Mathf.Clamp(movePos.y, -0.1f, 50f);
        }
            else
            {
                movePos.y -= gravity * Time.deltaTime;
            }


        //clamping camera y rotation
        lookInputs.x = Mathf.Clamp(lookInputs.x, -89, 89);

        //converting vector to Quaternion Euler
        headRot = Quaternion.Euler(lookInputs);

        //rotating the camera
        head.rotation = headRot;

        sinceLastGrounded += Time.deltaTime;
    }
    void Jump()
    {
        movePos.y += jumpHeight;
    }
    void FixedUpdate()
    {
        move.velocity = movePos;
    }

    void OnCollisionStay(Collision col)
    {
        foreach (var point in col.contacts)
        {
            if (point.normal.y > 0.2f && point.point.y < GetComponent<Collider>().bounds.center.y)
                sinceLastGrounded = 0;
        }
    }
}

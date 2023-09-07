using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_look : MonoBehaviour
{
    public float sensitivity;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;

    [SerializeField] private float camAngle;
    // euler angle of camera

    [SerializeField] private Transform camera;

    void Awake()
    {
        camAngle = 0;
    }

    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * 10;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * -10;
        // mouse input

        transform.Rotate(Vector3.up * mouseX);
        // sideways movement - the easy stuff...

        camAngle = Mathf.Clamp(camAngle + mouseY, -90, 90);

        Quaternion look = new Quaternion();
        look.eulerAngles = Vector3.right * camAngle;

        camera.localRotation = look;
    }
}

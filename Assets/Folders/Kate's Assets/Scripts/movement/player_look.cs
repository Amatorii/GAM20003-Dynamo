using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_look : MonoBehaviour
{
    public bool receiveInput = true;    

    public float sensitivity;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;

    [SerializeField] private float camAngle;
    // euler angle of camera

    [SerializeField] private Transform cam;

    void Awake()
    {
        camAngle = 0;
        EventManager.PlayerHasDied += DeathVoid;
    }

    private void DeathVoid()
    {
        receiveInput = false;
    }

    void Update()
    {
        if (receiveInput)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * 10;
            mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * -10;
            // mouse input

            transform.Rotate(Vector3.up * mouseX);
            // sideways movement - the easy stuff...

            camAngle = Mathf.Clamp(camAngle + mouseY, -90, 90);

            Quaternion look = new Quaternion();
            look.eulerAngles = Vector3.right * camAngle;

            cam.localRotation = look;

            Debug.DrawRay(cam.position, cam.forward, Color.blue);
        }
    }

    public void ChangeSensitivity(Slider slider) //takes value from pause menu sensitivity slider
    {
        sensitivity = slider.value;
    }
}

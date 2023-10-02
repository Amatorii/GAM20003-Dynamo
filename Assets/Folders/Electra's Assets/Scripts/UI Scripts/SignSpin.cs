using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SignSpin : MonoBehaviour
{
    public float rotationSpeed = 1f;
 
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotationSpeed/10, 0);
    }
}

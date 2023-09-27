using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISpin : MonoBehaviour
{
    private float rotateTime;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 lerped = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, 359.9f), 0.05f);

        transform.rotation = Quaternion.Euler(lerped);
        if (lerped.z < 0.5f)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}

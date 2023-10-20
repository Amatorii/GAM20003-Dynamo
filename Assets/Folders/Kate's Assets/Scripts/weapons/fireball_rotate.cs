using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_rotate : MonoBehaviour
{
    public float speed;

    // rotates the projectile along the z axis
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}

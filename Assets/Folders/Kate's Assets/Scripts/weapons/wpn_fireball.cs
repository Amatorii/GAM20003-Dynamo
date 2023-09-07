using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wpn_fireball : weapon_state
{
    Transform transform;
    GameObject projectile;

    public wpn_fireball(Transform transIn, GameObject projIn)
    {
        name = "fireball";

        transform = transIn;
        projectile = projIn;
    }

    public override void fire()
    {
        Object.Instantiate(projectile, transform.position + transform.forward * 0.5f, transform.rotation);
    }
}

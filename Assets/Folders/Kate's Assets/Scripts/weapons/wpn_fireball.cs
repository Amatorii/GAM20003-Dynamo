using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wpn_fireball : weapon_state
{
    public override void fire()
    {
        if (projectile != null)
            Object.Instantiate(projectile, transform.position + transform.forward * 0.5f, transform.rotation);
    }
}

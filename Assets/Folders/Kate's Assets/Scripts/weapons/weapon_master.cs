using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_master : MonoBehaviour
{
    public GameObject fireball;

    [SerializeField] private string activeWeapon;
    // debug

    weapon_state wpnActive;
    // current active weapon

    weapon_state wpnShotgun;
    weapon_state wpnFireball;

    void Awake()
    {
        wpnShotgun = new wpn_shotgun(transform);
        wpnFireball = new wpn_fireball(transform, fireball);

        wpnActive = wpnFireball;
        activeWeapon = wpnActive.name;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (wpnActive == wpnFireball)
                wpnActive = wpnShotgun;
            else
                wpnActive = wpnFireball;

            activeWeapon = wpnActive.name;
        }
        // this is a dev feature just so we can try these 2 out

        else if (Input.GetButtonDown("Fire1"))
            wpnActive.fire();
    }
}

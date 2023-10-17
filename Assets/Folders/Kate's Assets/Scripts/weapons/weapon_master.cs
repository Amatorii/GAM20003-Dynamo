using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_master : MonoBehaviour
{
    public GameObject fireball;
    public GameObject hitDecal;

    private GameObject currentWeapon;
    public List<GameObject> weapons;
    public int weaponIndex;

    public bool receiveInput = true;

    [SerializeField] private string activeWeapon;
    // debug

    weapon_state wpnActive;
    // current active weapon

    weapon_state wpnShotgun;
    weapon_state wpnFireball;
    List<weapon_state> weaponFunction;


    void Awake()
    {
        wpnShotgun = new wpn_shotgun(transform, hitDecal);
        wpnFireball = new wpn_fireball(transform, fireball);

        weaponFunction = new List<weapon_state> { wpnFireball, wpnShotgun };

        wpnActive = wpnFireball;
        activeWeapon = wpnActive.name;
        currentWeapon = weapons[0];
    }

    void Update()
    {
        if (receiveInput) 
        { 
        
            if (Input.mouseScrollDelta.y > 0)
            {
                weaponIndex++;
                if (weaponIndex == weapons.Count)
                    weaponIndex = 0;
                wpnActive = weaponFunction[weaponIndex];
                UpdateViewmodel(weapons[weaponIndex]);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                weaponIndex--;
                if (weaponIndex == -1)
                    weaponIndex = weapons.Count - 1;
                wpnActive = weaponFunction[weaponIndex];
                UpdateViewmodel(weapons[weaponIndex]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpdateViewmodel(weapons[0]);
                wpnActive = wpnFireball;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpdateViewmodel(weapons[1]);
                wpnActive = wpnShotgun;
            }

            //if (Input.GetButtonDown("Fire3"))
            //{
            //    if (wpnActive == wpnFireball)
            //    {
            //        wpnActive = wpnShotgun;
            //        UpdateViewmodel(weapons[1]);
            //    }
            //    else
            //    {
            //        wpnActive = wpnFireball;
            //        UpdateViewmodel(weapons[0]);
            //    }
            //
            //    activeWeapon = wpnActive.name;
            //}
            // this is a dev feature just so we can try these 2 out

            else if (Input.GetButtonDown("Fire1"))
                wpnActive.fire();
        
        }
    }

    void UpdateViewmodel(GameObject newWeapon)
    {
        currentWeapon.SetActive(false);
        currentWeapon = newWeapon;
        currentWeapon.SetActive(true);
    }
}

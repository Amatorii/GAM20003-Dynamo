using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_master : MonoBehaviour
{
    public bool receiveInput = true;
    // used for pausing

    [SerializeField] private weapon_state[] wpnList;
    // array of weapons

    int wpnActive;
    // current active weapon (defaults to 0)

    float cooldown; // for firing rate

    void Awake()
    {
        wpnActive = 0;
        cooldown = 0;
    }

    void Update()
    {
        if (receiveInput) 
        {
            cooldown = Mathf.MoveTowards(cooldown, 0, Time.deltaTime);

            #region changing weapons

            //scroll wheel
            if (Input.mouseScrollDelta.y > 0)
            {
                int i = wpnActive + 1;

                if (i >= wpnList.Length)
                    i = 0;

                ChangeWeapon(i);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                int i = wpnActive - 1;

                if (i < 0)
                    i = wpnList.Length -1;

                ChangeWeapon(i);
            }


            // number keys
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeWeapon(0);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeWeapon(1);
            #endregion

            // firing
            else if (cooldown == 0 && Input.GetButton("Fire1"))
            {
                wpnList[wpnActive].fire();
                cooldown = wpnList[wpnActive].fireRate;
            }
        }
    }

    void ChangeWeapon(int wpnNew) // sets new weapon
    {
        // enables the correct viewmodel and disables all others
        if (wpnList[wpnActive].model != null)
            wpnList[wpnActive].model.SetActive(false);

        if (wpnList[wpnActive].crosshair != null)
            wpnList[wpnActive].crosshair.SetActive(false);

        if (wpnList[wpnNew].model != null)
            wpnList[wpnNew].model.SetActive(true);

        if (wpnList[wpnNew].crosshair != null)
            wpnList[wpnNew].crosshair.SetActive(true);

        cooldown = wpnList[wpnNew].fireRate;

        wpnActive = wpnNew;

        Debug.Log("[" + name + "] Weapon Master: Switching active weapon to " + wpnList[wpnActive].name + ".");
    }
}
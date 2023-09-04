using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Electra.weapon
{
    public class WeaponSwitch : MonoBehaviour
    {
        public GameObject activeWeapon;

        public List<GameObject> weaponList;
        public int weaponIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            //weaponList = FindObjectsByType<>
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                if (weaponIndex == 0)
                    Weapon1();
                else if (weaponIndex == 1)
                    Weapon2();
                else if (weaponIndex == 2)
                    Weapon3();
                weaponIndex++;
                if (weaponIndex == 3)
                    weaponIndex = 0;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                if (weaponIndex == 0)
                    Weapon1();
                else if (weaponIndex == 1)
                    Weapon2();
                else if (weaponIndex == 2)
                    Weapon3();
                weaponIndex--;
                if (weaponIndex == -1)
                    weaponIndex = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                Weapon1();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Weapon2();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Weapon3();
        }

        public void Weapon1()
        {
            activeWeapon.SetActive(false);
            activeWeapon = weaponList[0];
            activeWeapon.SetActive(true);
        }
        public void Weapon2()
        {
            activeWeapon.SetActive(false);
            activeWeapon = weaponList[1];
            activeWeapon.SetActive(true);
        }
        public void Weapon3()
        {
            activeWeapon.SetActive(false);
            activeWeapon = weaponList[2];
            activeWeapon.SetActive(true);
        }
    }
}

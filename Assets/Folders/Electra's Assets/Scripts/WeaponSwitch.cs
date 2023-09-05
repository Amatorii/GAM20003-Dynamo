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
                weaponIndex++;
                if (weaponIndex == 3)
                    weaponIndex = 0;
                SwapWeapon(weaponList[weaponIndex]);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                weaponIndex--;
                if (weaponIndex == -1)
                    weaponIndex = 2;
                SwapWeapon(weaponList[weaponIndex]);
            }

            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //    SwapWeapon(shotgun);
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //    SwapWeapon(rocketLauncher);
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            //    SwapWeapon(thirdGun);
        }
        public void SwapWeapon(GameObject w)
        {
            activeWeapon.SetActive(false);
            activeWeapon = w;
            activeWeapon.SetActive(true);
        }
    }
}

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
                
                SwapWeapon(weaponIndex);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                weaponIndex--;
                if (weaponIndex == -1)
                    weaponIndex = 2;

                SwapWeapon(weaponIndex);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwapWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwapWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwapWeapon(2);
        }

        public void SwapWeapon(int w)
        {
            activeWeapon.SetActive(false);
            activeWeapon = weaponList[w];
            activeWeapon.SetActive(true);
        }
    }
}

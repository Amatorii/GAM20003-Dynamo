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
      
        void Update()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                weaponIndex++;
                if (weaponIndex == weaponList.Count)
                    weaponIndex = 0;
                SwapWeapon(weaponList[weaponIndex]);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                weaponIndex--;
                if (weaponIndex == -1)
                    weaponIndex = weaponList.Count;
                SwapWeapon(weaponList[weaponIndex]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwapWeapon(weaponList[0]);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwapWeapon(weaponList[1]);
            
        }
        public void SwapWeapon(GameObject w)
        {
            activeWeapon.SetActive(false);
            activeWeapon = w;
            activeWeapon.SetActive(true);
        }
    }
}

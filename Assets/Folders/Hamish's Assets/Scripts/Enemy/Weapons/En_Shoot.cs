using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Hamish.Enemy
{
    public class En_Shoot
    {
        [Header("Gun Variables")]
        [SerializeField] private Transform nozzle;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float damage, rpm;

        protected IEnumerator ShootGun()
        {
            while (true)
            {
                GameObject _bullet = GameObject.Instantiate(bullet, nozzle.position + nozzle.forward, nozzle.rotation);
                yield return new WaitForSeconds(rpm);
            }
        }
    }
}


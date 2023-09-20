using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hamish.Enemy
{
    public class DummyEnemy : Enemy
    {
        private bool currentlyShooting = true;
        [SerializeField]private GameObject gun;

        protected override void Awake()
        {
            base.Awake();
        }

        void Update()
        {
            if (_canSeePlayer)
            {

                AimGun();
                if (Vector3.Distance(playerObject.transform.position, transform.position) > 5.0f)
                    _agent.SetDestination(playerObject.transform.position);
                else
                    _agent.SetDestination(transform.position);

                Debug.Log("[" + this + "]: Sees the player");
            }
            Debug.DrawRay(gun.transform.position, gun.transform.TransformDirection(Vector3.forward), Color.red, Mathf.Infinity);
        }

        private void AimGun()
        {
            RaycastHit _hit;
            bool _isAimed = false;

            Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.forward), out _hit, Mathf.Infinity);

            if(_hit.collider != null)
                _isAimed = _hit.collider.CompareTag("Player");

            if (currentlyShooting && _isAimed)
            {
                currentlyShooting = false;
                StartCoroutine(ShootGun(4));
            }
            if (lookAtPlayer != null)
                StopCoroutine(lookAtPlayer);
            lookAtPlayer = StartCoroutine(LookAtPlayer());
        }

        private IEnumerator LookAtPlayer()
        {
            Quaternion lookRotation = Quaternion.LookRotation(playerObject.transform.position - gun.transform.position);
            float time = 0;


            while (time < 1)
            {
                gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, lookRotation, time);
                time += Time.deltaTime * 1.0f;
                Debug.Log("HI");
                yield return null;
            }
        }

        [Header("Gun Variables")]
        [SerializeField] private Transform nozzle;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float damage, rpm;
        private Coroutine lookAtPlayer;

        private IEnumerator ShootAtPlayer()
        {
            while (true)
            {
                if (_canSeePlayer)
                {
                    StartCoroutine(ShootGun(4));

                    yield return new WaitForSeconds(5.0f);
                }
            }
        }

        private IEnumerator ShootGun(int _noBullets)
        {
            while (0 != _noBullets)
            {
                GameObject _bullet = GameObject.Instantiate(bullet, nozzle.position + nozzle.forward, transform.rotation);
                yield return new WaitForSeconds(60/rpm);
                _noBullets--;
            }
            yield return new WaitForSeconds(1.0f);
            currentlyShooting = true;
        }
    }
}
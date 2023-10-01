using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class ent_rangedEnemy : Enemy
    {
        private bool currentlyShooting = true;
        [SerializeField] private GameObject gun;
        public bool runStateMachine;
        [SerializeField] private EnemyState currentState;

        protected override void Awake()
        { 
            base.Awake();
            StartStateMachine(new EnemyIdle(this));
            Debug.Log(this);
            isAttacking = false;
        }

        private void Update()
        {
            RunStateMachine();
        }

        #region StateMachine

        private void StartStateMachine(EnemyState state)
        {
            currentState = state;
            currentState.RunState();
        }

        private void RunStateMachine()
        {
            EnemyState nextState = currentState?.RunState();
            currentState = nextState;
            Debug.Log(currentState);
        }
        #endregion
        
        public override EnemyState AttackPlayer()
        {
            RaycastHit _hit;
            bool _isAimed = false;

            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, Mathf.Infinity);
            
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red, Mathf.Infinity);

            if (_hit.collider != null)
                _isAimed = _hit.collider.CompareTag("Player");

            if (currentlyShooting && _isAimed)
            {
                StartCoroutine(ShootGun(3));
                currentlyShooting = false;
            }
            if(!_isAimed)
                return new EnemyChase(this);
            return new EnemyAttack(this);
        }

        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform nozzle;
        [SerializeField] private float rpm;
        private IEnumerator ShootGun(int _noBullets)
        {
            while (0 != _noBullets)
            {
                GameObject _bullet = GameObject.Instantiate(bullet, nozzle.position + nozzle.forward, transform.rotation);
                yield return new WaitForSeconds(60 / rpm);
                _noBullets--;
            }
            yield return new WaitForSeconds(1.0f);
            currentlyShooting = true;
        }
        /*
        private IEnumerator ShootAtPlayer(Action<int> callback)
        {
            isAttacking = true;
            Debug.Log("Shooting");
            yield return new WaitForSeconds(2);
            callback(1);
            isAttacking = false;
            yield return null;
        }
        */
    }
}
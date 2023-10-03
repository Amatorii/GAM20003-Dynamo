using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class ent_rangedEnemy : Enemy
    {
        private bool currentlyShooting = true;
        [Header("Class Specific Variables")]
        [SerializeField] private GameObject gun;
        public bool runStateMachine;
        [SerializeField]private EnemyState currentState;

        protected override void Awake()
        { 
            base.Awake();
            StartStateMachine(new EnemyIdle(this));
            Debug.Log(this);
            isAttacking = false;
        }

        private void Update()
        {
            StartCoroutine(LookAtPlayer());
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
            
            return new EnemyMove(this);
        }

        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform gunNozzle;
        [SerializeField] private float rpm;
        [SerializeField] private float xModifier;
        [SerializeField] private float yModifier;
        [SerializeField] private float zModifier;
        private IEnumerator ShootGun(int _noBullets)
        {
            while (0 != _noBullets)
            {
                GameObject _bullet = GameObject.Instantiate(bullet, gunNozzle.position + gunNozzle.forward, transform.rotation);
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
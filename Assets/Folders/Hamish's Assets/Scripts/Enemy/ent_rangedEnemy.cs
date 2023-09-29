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
            EnemyState nextState = null;
            if (!isAttacking)
            {
                StartCoroutine(ShootAtPlayer(result => { nextState = new EnemyChase(this); }));
                if (nextState != null)
                {
                    Debug.Log("Successfully Returned");
                    return nextState;
                }
            }
            return currentState;
        }

        private IEnumerator ShootAtPlayer(Action<int> callback)
        {
            isAttacking = true;
            Debug.Log("Shooting");
            yield return new WaitForSeconds(2);
            callback(1);
            isAttacking = false;
            yield return null;
        }
    }
}
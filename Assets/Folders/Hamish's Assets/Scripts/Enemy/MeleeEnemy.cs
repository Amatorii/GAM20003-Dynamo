using Hamish.player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class MeleeEnemy : Enemy
    {
        private BoxCollider attackHitBox;
        private bool canAttackPlayer;
        protected override void Awake()
        {
            base.Awake();
            attackHitBox = GetComponentInChildren<BoxCollider>();
        }

        // Update is called once per frame
        private void Update()
        {
            RunStateMachine();
        }

        public override EnemyState AttackPlayer()
        {
            if (!isAttacking)
            {
                StartCoroutine(MeleeAttack());
                isAttacking = true;
                Debug.Log("I'm trying to attack the player");
            }
            return currentState;
        }

        private IEnumerator MeleeAttack()
        {
            if(canAttackPlayer)
            {
                PlayerController playerScript = playerObject.GetComponent<PlayerController>();
                playerScript.IveBeenAttacked();
                yield return new WaitForSeconds(1);
            }
            isAttacking = false;
            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                canAttackPlayer = true;
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
                canAttackPlayer = false;
        }
    }
}
using Hamish.player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class MeleeEnemy : Enemy
    {
        public string ShowCurrentState;
        private BoxCollider attackHitBox;
        private bool canAttackPlayer;
        protected override void Awake()
        {
            base.Awake();
            attackHitBox = GetComponentInChildren<BoxCollider>();
            _agent.stoppingDistance = 3;
        }

        // Update is called once per frame
        private void Update()
        {
            RunStateMachine();
            ShowCurrentState = currentState.ToString();

            float distanceToPlayer = Vector3.Distance(playerObject.transform.position, transform.position);
            if (distanceToPlayer < 2)
                _agent.speed = 1;
            else if (distanceToPlayer < 5)
            {
                _agent.speed = distanceToPlayer / 1.2f;
            }
            else
                _agent.speed = distanceToPlayer * 1.5f;
            Debug.Log("Distance to player= "+distanceToPlayer);
        }

        public override EnemyState AttackPlayer()
        {
            LookAtPlayer();
            if (!isAttacking)
            {
                StartCoroutine(MeleeAttack());
                isAttacking = true;
            }
            return currentState;
        }

        private IEnumerator MeleeAttack()
        {
            if(canAttackPlayer)
            {
                ent_health playerScript = playerObject.GetComponent<ent_health>();
                playerScript.Damage(damage);
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
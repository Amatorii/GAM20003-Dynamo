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
        [SerializeField]private bool canAttackPlayer;

        protected override void Awake()
        {
            base.Awake();
            attackHitBox = GetComponentInChildren<BoxCollider>();
        }

        private void Update()
        {
            if (healthScript.health <= 0)
            {
                Death();
                return;
            }

            RunStateMachine();
            ShowCurrentState = currentState.ToString();
        }

        public override EnemyState AttackPlayer()
        {
            LookAtPlayer();
            if (!isAttacking && canAttackPlayer)
            {
                StartCoroutine(MeleeAttack());
                isAttacking = true;
                _agent.isStopped = true;
                return new EnemyAttack(this);
            }
            else
            {
                _agent.isStopped = false;
                return new EnemyMove(this);
            }
        }

        public override EnemyState MoveToPlayer()
        {
            _agent.SetDestination(PredictedPosition());
            if (canAttackPlayer)
                return new EnemyAttack(this);

            return new EnemyMove(this);
        }


        private Vector3 PredictedPosition()
        {
            Vector3 predictedPosition = playerObject.transform.position + playerObject.GetComponent<state_manager>().velocity / 5;
            Debug.DrawLine(transform.position, predictedPosition, Color.magenta);

            return predictedPosition;
        }

        private IEnumerator MeleeAttack()
        {
            animator.SetInteger("CurrentState", 3);
            if(canAttackPlayer)
            {
                animator.Play("Punching");
                ent_health playerScript = playerObject.GetComponent<ent_health>();
                playerScript.Damage(damage);
                yield return new WaitForSeconds(1);
            }
            isAttacking = false;
            yield return null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other != null)
            {
                if (other.CompareTag("Player"))
                    canAttackPlayer = true;
                else
                    canAttackPlayer = false;
                
            }
            else
                canAttackPlayer = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other != null && other.CompareTag("Player"))
                canAttackPlayer = false;
        }
    }
}
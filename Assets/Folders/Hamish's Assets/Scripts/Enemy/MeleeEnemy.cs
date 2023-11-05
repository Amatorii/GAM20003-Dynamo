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
            if (!isAttacking)
            {
                StartCoroutine(MeleeAttack());
                isAttacking = true;
            }
            return currentState;
        }

        public override EnemyState MoveToPlayer()
        {
            _agent.SetDestination(PredictedPosition());
            Physics.BoxCast(attackHitBox.center, attackHitBox.size, transform.forward, out RaycastHit hitinfo);
            if (hitinfo.collider != null)
            {
                Debug.LogWarning($"[{name}] Hit info = {hitinfo.collider.tag}");
                if (hitinfo.collider.CompareTag("Player"))
                    return new EnemyAttack(this);
            }
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
    }
}
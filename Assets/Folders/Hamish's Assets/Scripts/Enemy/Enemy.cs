using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Runtime.InteropServices;
using System;


namespace Hamish.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        protected Rigidbody rb;
        protected CapsuleCollider hitBox;
        protected NavMeshAgent _agent;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            hitBox = GetComponent<CapsuleCollider>();
            _agent = GetComponent<NavMeshAgent>();
            playerObject = GameObject.FindGameObjectWithTag("Player");

            StartCoroutine(FOVRoutine());
            Debug.Log("[" + this + "]: Sucessfully Initialized");
        }

        [Header("Vision Cone")]
        public float _radius;
        public float _angle;
        public float _delay = 1.0f;
        public bool _canSeePlayer;
        public bool isAttacking = false;

        public GameObject playerObject;

        public LayerMask targetMask;
        public LayerMask obstructionMask;
        protected Vector3 directToTarget;

        protected IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(_delay);
            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        public void MoveToPlayer()
        {
            _agent.SetDestination(playerObject.transform.position);
        }

        public abstract EnemyState AttackPlayer();

        private void FieldOfViewCheck() //FOV for finding Player
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                directToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directToTarget) < _angle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directToTarget, distToTarget, obstructionMask))
                        _canSeePlayer = true;
                    else
                        _canSeePlayer = false;
                }
                else
                    _canSeePlayer = false;
            }
            else if (_canSeePlayer)
                _canSeePlayer = false;
        }
    }
}
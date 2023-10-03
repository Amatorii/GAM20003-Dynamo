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
            Debug.Log("[" + this + "]: transform.right = " + transform.right);

        }

        [Header("Vision Cone")]
        public float _radius;
        public float _angle;
        public float _delay = 1.0f;
        public bool _canSeePlayer;
        public bool isAttacking = false;
        [Header("Masks")]
        public GameObject playerObject;

        public LayerMask targetMask;
        public LayerMask obstructionMask;
        protected Vector3 directToTarget;

        [Header("NavMeshPath")]
        public Vector3 currentPath;

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

        public void Strafe()
        {
            _agent.SetDestination(_agent.transform.position-(transform.right*2));
        }

        public void MoveAwayFromPlayer()
        {
            _agent.SetDestination(-playerObject.transform.position);
        }

        public void StopAgent()
        {
            _agent.SetDestination(transform.position);
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

        protected IEnumerator LookAtPlayer()
        {
            Quaternion lookRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
                time += Time.deltaTime * 1.0f;
                yield return null;
            }
        }

        public bool IsEnemyMoving()
        {
            if (_agent.hasPath)
                return true;
            else
                return false;
        }
    }
}
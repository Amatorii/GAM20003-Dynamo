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
        private Rigidbody rb;
        private CapsuleCollider hitBox;
        private NavMeshAgent _agent;


        public virtual void Init()
        {
            rb = GetComponent<Rigidbody>();
            hitBox = GetComponent<CapsuleCollider>();
            _agent = GetComponent<NavMeshAgent>();
            playerObject = GameObject.FindGameObjectWithTag("Player");

            StartCoroutine(FOVRoutine());
        }

        private void Update()
        {
            if (_canSeePlayer)
            {
                _agent.SetDestination(playerObject.transform.position);
            }
        }

        [Header("Vision Cone")]
        public float _radius;
        public float _angle;
        private float _delay;
        public bool _canSeePlayer;

        public GameObject playerObject;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(_delay);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck() //FOV for finding Player
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directToTarget = (target.position - transform.position).normalized;
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
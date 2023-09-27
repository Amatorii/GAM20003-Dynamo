using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hamish.Enemy
{
    public abstract class EnemyClass : MonoBehaviour
    {
        protected Rigidbody rb;
        protected CapsuleCollider hitBox;
        protected NavMeshAgent _agent;
        [SerializeField]private GameObject playerObject;

        //protected GameObject enemyModel;
        protected Animator enemyAnimation;

        private bool debugActive = false;

        private bool IsWalking = false;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            EchoDebug("RigidBody = " + rb);
            hitBox = GetComponent<CapsuleCollider>();
            EchoDebug("Hitbox = " + hitBox);
            _agent = GetComponent<NavMeshAgent>();
            EchoDebug("NavMesh = " + _agent);
            playerObject = GameObject.FindGameObjectWithTag("Player");

            //enemyModel = GetComponentInChildren<GameObject>();
            enemyAnimation = GetComponentInChildren<Animator>();
        }

        private void EchoDebug(object msg)
        {
            if (debugActive)
            {
                Debug.Log("["+this+"]"+msg);
            }
        }
    }
}
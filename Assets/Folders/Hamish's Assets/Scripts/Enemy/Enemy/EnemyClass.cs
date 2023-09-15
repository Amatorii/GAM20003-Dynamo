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
        public GameObject playerObject;

        private bool debugActive = true;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            EchoDebug("[" + this + "]: RigidBody = " + rb);
            hitBox = GetComponent<CapsuleCollider>();
            EchoDebug("[" + this + "]: Hitbox = " + hitBox);
            _agent = GetComponent<NavMeshAgent>();
            EchoDebug("[" + this + "]: NavMesh = " + _agent);
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }



        private void EchoDebug(object msg)
        {
            if (debugActive)
            {
                Debug.Log(msg);
            }
        }
    }
}
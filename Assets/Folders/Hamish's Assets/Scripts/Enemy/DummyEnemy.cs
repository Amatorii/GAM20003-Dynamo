using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hamish.Enemy
{
    public class DummyEnemy : MonoBehaviour
    {
        private Transform playerPosition;
        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            agent.destination = playerPosition.position;
        }
    }
}
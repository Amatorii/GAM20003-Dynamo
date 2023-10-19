using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestStopping : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            agent.SetDestination(target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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
        protected Animator animator;

        [Header("Stats")]
        /// <summary>
        /// Becareful, the ranged enemy applies it's damage for every bullet
        /// You can change it so the player's script decides how much damage it takes from attacks but atm, the enemies call the player's health script
        /// </summary>
        [Range(0, 50)][SerializeField]protected int damage;
        /// <summary>
        /// How fast the AI Looks at the player
        /// </summary>
        [Range(10, 50)][SerializeField]protected int turningSpeed;
        [Range(3.5f, 10.0f)][SerializeField]protected float moveSpeed;
        /// <summary>
        /// This is mainly for the Ranged Enemy
        /// To make the AI more deadly, make this bigger, increase the bullet speed and make it's turning speed higher
        /// The AI is more accurate when standing still
        /// </summary>
        [Range(0, 100)][SerializeField] public int maxDistanceToChase;

        protected ent_health healthScript;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            hitBox = GetComponent<CapsuleCollider>();
            _agent = GetComponent<NavMeshAgent>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponentInChildren<Animator>();
            StartCoroutine(FOVRoutine());
            StartStateMachine(new EnemyIdle(this));

            _agent.speed = moveSpeed;

            healthScript = GetComponent<ent_health>();
        }

        #region StateMachine
        [SerializeField] protected EnemyState currentState;
        protected void StartStateMachine(EnemyState state)
        {
            currentState = state;
            currentState.RunState();
        }

        protected void RunStateMachine()
        {
            EnemyState nextState = currentState?.RunState();
            currentState = nextState;
            Debug.Log(currentState);
        }
        #endregion

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
            StoppingDistanceFix();
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
            _agent.isStopped = true;
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
/*
        protected void LookAtPlayer()
        {
            Quaternion lookRotation = Quaternion.LookRotation((playerObject.transform.position) - transform.position);
            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
                time += Time.deltaTime * 0.5f;
            }
        }
*/
        
        protected void LookAtPlayer()
        {
            Vector3 dir = playerObject.transform.position - transform.position;
            dir.y = 0;

            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turningSpeed * Time.deltaTime);
        }
        public bool IsEnemyMoving()
        {
            if (_agent.hasPath)
                return true;
            else
                return false;
        }

        protected void StoppingDistanceFix()
        {
            bool bDistance = _agent.remainingDistance > _agent.stoppingDistance;
            _agent.isStopped = false;
            if (!bDistance)
            {
                StopAgent();
            }
        }

        protected void Death()
        {
            currentState = new EnemyDeath(this);
            _agent.speed = 0;
            //StopAllCoroutines();
            animator.SetBool("IsDead", true);
            StartCoroutine(FinalCountDown());
        }

        private IEnumerator FinalCountDown()
        {
            yield return new WaitForSeconds(5);
            Debug.Log("[" + this.name + "] destroying self");
            Destroy(this.gameObject);
        }

        public void SetAnimationState(int state) { animator.SetInteger("CurrentState", state); }
    }
}
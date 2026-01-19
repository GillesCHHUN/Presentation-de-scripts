using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.StateMachine
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        public EnemyStateMachine StateMachine => _stateMachine;

        public NavMeshAgent Agent { get; private set; }
        public PlayerReferenceSO PlayerRef;
        [field: SerializeField] public LayerMask WhatIsPlayer { get; private set; }
        [field: SerializeField] public float RadiusDetector { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
        public EnemyEventAnimation EventAnimation { get; private set; }
        public EnemyHealth Health { get; private set; }
        
        public bool IsPlayerDead;
        public Animator Animator { get; private set; }
        //private float t;

        private void Awake()
        {
            _stateMachine = new EnemyStateMachine(this);
            Animator = GetComponentInChildren<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            EventAnimation = GetComponentInChildren<EnemyEventAnimation>();
            Health = GetComponent<EnemyHealth>();
            
        }

        private void Start()
        {
            _stateMachine.Initialize(_stateMachine.idleState);
        }

        private void Update()
        {
            _stateMachine.Execute();
        }

        public bool PlayerInAttackRange()
        {
            return Physics.CheckSphere(Agent.transform.position,
                AttackRadius, WhatIsPlayer);
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.TryGetComponent(out IDamageablePlayer target))
        //     {
        //         CurrentTarget = target;
        //     }
        // }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, RadiusDetector);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, PlayerRef.TransformSO.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, AttackRadius);
        }

    }
}
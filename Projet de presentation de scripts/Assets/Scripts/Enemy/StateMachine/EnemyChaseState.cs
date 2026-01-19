using UnityEngine;

namespace Enemy.StateMachine
{
    public class EnemyChaseState : IState
    {
        private Enemy enemy;
        private float crossFadeDuration = 0.1f;
        private readonly int IsChasingHash = Animator.StringToHash("IsChasing");
        private readonly int ChasingHash = Animator.StringToHash("Chase");

        public EnemyChaseState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void Enter()
        {
            enemy.Animator.SetBool(IsChasingHash, true);
            enemy.Animator.CrossFadeInFixedTime(ChasingHash, crossFadeDuration);
            //Debug.Log("enter chase state");
        }

        public void Execute()
        {
            if (enemy.PlayerRef.IsPlayerDeadSO)
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.idleState);
                return;
            }
            
            ChasePlayer();

            if (enemy.PlayerInAttackRange())
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.attackState);

            if (enemy.Health.currentHealth <= 0)
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.deadState);
            
            
        }

        public void Exit()
        {
            enemy.Animator.SetBool(IsChasingHash, false);
            //Debug.Log("Exit chase state");
        }


        private void ChasePlayer()
        {
            if (enemy.PlayerRef != null && enemy.Agent != null && enemy.Agent.isOnNavMesh)
            {
                enemy.Agent.SetDestination(enemy.PlayerRef.TransformSO.position);
            }
        }
        
        
    }
}
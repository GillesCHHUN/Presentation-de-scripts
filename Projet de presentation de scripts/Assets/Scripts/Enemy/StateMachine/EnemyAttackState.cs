using UnityEngine;


namespace Enemy.StateMachine
{
    public class EnemyAttackState : IState
    {
        private Enemy enemy;
        private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
        private readonly int AttackingHash = Animator.StringToHash("Attack02");
        private int randomAttack;
        private float crossFadeDuration = 0.1f;
        private float t;

        public EnemyAttackState(Enemy enemy)
        {
            this.enemy = enemy;
        }
        
        public void Enter()
        {
            enemy.EventAnimation.IsAttacking = true;
            enemy.Animator.SetBool(IsAttackingHash, true);
            enemy.Animator.CrossFadeInFixedTime(AttackingHash, crossFadeDuration);
            //Debug.Log("Enter attack State");
        }

        public void Execute()
        {
            if (enemy.PlayerRef.IsPlayerDeadSO)
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.idleState);
                return;
            }
            
            FacePlayer();

            if (enemy.EventAnimation.IsAttacking)
            {
                enemy.Agent.isStopped = true;
            }
            
            if (!enemy.PlayerInAttackRange() && !enemy.EventAnimation.IsAttacking)
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.chaseState);
            }
            
            if(enemy.Health.currentHealth <= 0)
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.deadState);
            
            
        }

        public void Exit()
        {
            enemy.Agent.isStopped = false;
            enemy.Animator.SetBool(IsAttackingHash, false);
           // Debug.Log("Exit attack state");
        }

        private void FacePlayer()
        {
            float lerpTime = .1f;
            
            Vector3 dirToPlayer = (enemy.PlayerRef.TransformSO.position - enemy.transform.position).normalized;
            dirToPlayer.y = 0;
            
            enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dirToPlayer), lerpTime );
        }
        
    }
}


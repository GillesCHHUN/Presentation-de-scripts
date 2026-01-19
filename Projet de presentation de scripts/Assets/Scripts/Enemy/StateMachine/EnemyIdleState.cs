using UnityEngine;


namespace Enemy.StateMachine
{
    public class EnemyIdleState : IState
    {
        private Enemy enemy;
        private readonly int IsIdleHash = Animator.StringToHash("IsIdle");
        private readonly int IdleHash = Animator.StringToHash("Idle03");
        private float crossFadeDuration = 0.1f;
        
        
        public EnemyIdleState(Enemy enemy)
        {
            this.enemy = enemy;
        }
        
        public void Enter()
        {
            enemy.Animator.SetBool(IsIdleHash, true);
            enemy.Animator.CrossFadeInFixedTime(IdleHash, crossFadeDuration);
        }

        public void Execute()
        {
            if (enemy.PlayerRef.IsPlayerDeadSO)
                return;

            if (enemy.Health.currentHealth <= 0)
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.deadState);
            }
            
            if (IsPlayerIsNearbyAndDetectable())
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.screamState);
            }
            
            
        }

        public void Exit()
        {
            enemy.Animator.SetBool(IsIdleHash, false);
            //Debug.Log("exit idle state");
        }

        private bool IsPlayerIsNearbyAndDetectable()
        {
            float distanceToPlayer = Vector3.Distance(enemy.PlayerRef.TransformSO.position , enemy.transform.position);
            
            if (distanceToPlayer <= enemy.RadiusDetector)
            {
                Vector3 dirToPlayer = (enemy.PlayerRef.TransformSO.position - enemy.transform.position).normalized;

                if (Physics.Raycast(enemy.transform.position, dirToPlayer, out RaycastHit hit))
                {
                    if(hit.collider == enemy.PlayerRef.ColliderSO)
                        return true;
                }
            }
      
            return false; 
        }
        
    }
}



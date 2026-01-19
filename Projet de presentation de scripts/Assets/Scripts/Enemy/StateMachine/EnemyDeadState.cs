using UnityEngine;

namespace Enemy.StateMachine
{
    public class EnemyDeadState : IState
    {
        private Enemy enemy;
        private readonly int IsDeadHash = Animator.StringToHash("IsDead");
        private readonly int DeadHash = Animator.StringToHash("Death");
        private float crossFadeDuration = 0.1f;
        
        
        public EnemyDeadState(Enemy enemy)
        {
            this.enemy = enemy;
        }
        
        public void Enter()
        {
            enemy.Animator.SetTrigger(IsDeadHash);
            enemy.Animator.CrossFadeInFixedTime(DeadHash, crossFadeDuration);
            enemy.GetComponent<Collider>().enabled = false;
           // Debug.Log("IsDead state");
        }

        public void Execute()
        {
            enemy.Agent.isStopped = true;
        }

        public void Exit()
        {
            
        }
    }
}


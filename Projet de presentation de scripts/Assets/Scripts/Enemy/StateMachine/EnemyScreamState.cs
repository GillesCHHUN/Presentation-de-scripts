using UnityEngine;


namespace Enemy.StateMachine
{
    public class EnemyScreamState : IState
    {
        private Enemy enemy;
        private readonly int IsScreamingHash = Animator.StringToHash("IsScreaming");
        private readonly int ScreamingHash = Animator.StringToHash("Scream");
        private float crossFadeDuration = 0.1f;
        private float timer;
        private float screamDuration = 2.0f;

        public EnemyScreamState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void Enter()
        {
            enemy.Animator.SetBool(IsScreamingHash, true);
            enemy.Animator.CrossFadeInFixedTime(ScreamingHash, crossFadeDuration);
            timer = screamDuration;
        }

        public void Execute()
        {
            if (enemy.PlayerRef.IsPlayerDeadSO)
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.idleState);
                return;
            }
            
            FacePlayer();
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.chaseState);
            }
            
            if (enemy.Health.currentHealth <= 0)
                enemy.StateMachine.SwitchStateTo(enemy.StateMachine.deadState);
        }

        public void Exit()
        {
            enemy.Animator.SetBool(IsScreamingHash, false);
        }

        private void FacePlayer()
        {
            if (enemy.PlayerRef != null)
            {
                Vector3 dirToPlayer = (enemy.PlayerRef.TransformSO.position - enemy.transform.position).normalized;
                dirToPlayer.y = 0;

                float rotationSpeed = 8f;
                enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation,
                    Quaternion.LookRotation(dirToPlayer),
                    rotationSpeed * Time.deltaTime);
            }
        }
    }
}
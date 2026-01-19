using System;

namespace Enemy.StateMachine
{
    
    public class EnemyStateMachine
    {
        public IState CurrentState { get; private set; }
        public event Action<IState> stateChanged;

        public EnemyChaseState chaseState;
        public EnemyAttackState attackState;
        public EnemyIdleState  idleState;
        public EnemyDeadState deadState;
        public EnemyScreamState screamState;

        public EnemyStateMachine(Enemy enemy)
        {
            this.idleState = new EnemyIdleState(enemy);
            this.chaseState = new EnemyChaseState(enemy);
            this.attackState = new EnemyAttackState(enemy);
            this.deadState = new EnemyDeadState(enemy);
            this.screamState = new EnemyScreamState(enemy);
        }

        public void Initialize(IState state)
        {
            CurrentState =  state;
            state.Enter();
            
            stateChanged?.Invoke(state);
        }

        public void SwitchStateTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }

        public void Execute()
        {
            if (CurrentState != null)
            {
                CurrentState.Execute();
            }
        }

    }
}


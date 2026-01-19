using UnityEngine;


namespace Enemy.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Execute();
        public void Exit();
    }
}


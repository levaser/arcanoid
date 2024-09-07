using System;

namespace Utility.StateSystem
{
    public abstract class State
    {
        protected IStateMachine StateMachine;
        private readonly Lazy<IStateMachine> _stateMachineLazy;

        public event Action StateEntered;
        public event Action StateExited;

        public State(Lazy<IStateMachine> stateMachine) => _stateMachineLazy = stateMachine;

        public void Enter()
        {
            StateMachine = _stateMachineLazy.Value;

            OnEnter();
            StateEntered?.Invoke();
        }

        public void Exit()
        {
            OnExit();
            StateExited?.Invoke();
        }

        protected virtual void OnEnter() {}
        public virtual void Update() {}
        protected virtual void OnExit() {}
    }
}
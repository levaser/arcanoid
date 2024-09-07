using System;

namespace Utility.StateSystem
{
    public abstract class StateView<T> : IDisposable where T : State
    {
        protected readonly T TargetState;

        public StateView(T targetState)
        {
            TargetState = targetState;

            TargetState.StateEntered += OnTargetStateEntered;
            TargetState.StateExited += OnTargetStateExited;
        }

        public void Dispose()
        {
            TargetState.StateEntered -= OnTargetStateEntered;
            TargetState.StateExited -= OnTargetStateExited;
        }

        protected virtual void OnTargetStateEntered() { }
        protected virtual void OnTargetStateExited() { }
    }
}
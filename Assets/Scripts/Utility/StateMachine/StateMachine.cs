using System;
using System.Collections.Generic;
using System.Linq;
using VContainer.Unity;

namespace Utility.StateSystem
{
    public abstract class StateMachine<T> : IStateMachine, IFixedTickable where T : State
    {
        public event Action<T> StateChanged;

        protected readonly List<T> States;

        public T State { get; protected set; }

        public StateMachine(IEnumerable<T> states)
        {
            States = states.ToList();
        }

        public void FixedTick()
        {
            State.Update();
            Update();
        }

        protected virtual void Update() { }

        public void SetState(T state)
        {
            if (State != null)
                State.Exit();
            State = state;
            State.Enter();

            StateChanged?.Invoke(State);
        }

        public void SetState<A>() where A : State
        {
            SetState((States.Find(state => state is A))
                ?? throw new NullReferenceException($"States does not contain state of type {typeof(A)}"));
            // UnityEngine.Debug.Log($"{typeof(A)}");
        }
    }
}
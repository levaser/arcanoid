using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class BallStateMachine : Utility.StateSystem.StateMachine<BallState>, IStartable
    {
        [Inject]
        public BallStateMachine(IEnumerable<BallState> states) : base(states)
        {
        }

        void IStartable.Start()
        {
            SetState<OnPlatformState>();
        }
    }
}
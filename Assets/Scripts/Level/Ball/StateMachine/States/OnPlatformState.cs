using System;
using UnityEngine;
using Utility.StateSystem;
using VContainer;

namespace Game.Levels
{
    public sealed class OnPlatformState : BallState
    {
        private readonly PlatformMover _platformMover;

        [Inject]
        public OnPlatformState(
            Lazy<IStateMachine> stateMachine,
            LevelInput input,
            Transform ballTransform,
            PlatformMover platformMover
        ) : base(stateMachine, input, ballTransform)
        {
            _platformMover = platformMover;
        }

        protected override void OnEnter()
        {
            Input.LevelStartPerformed += OnLevelStartPerformed;
            _platformMover.PlatformMoved += OnPlatformMoved;
        }

        protected override void OnExit()
        {
            Input.LevelStartPerformed -= OnLevelStartPerformed;
            _platformMover.PlatformMoved -= OnPlatformMoved;
        }

        private void OnLevelStartPerformed()
        {
            StateMachine.SetState<AttackState>();
        }

        private void OnPlatformMoved(float newPlatformXPos)
        {
            BallTransform.position = new Vector3(
                newPlatformXPos,
                BallTransform.position.y,
                BallTransform.position.z
            );
        }
    }
}
using System;
using UnityEngine;
using Utility.StateSystem;
using VContainer;

namespace Game.Levels
{
    public sealed class OnPlatformState : BallState
    {
        private readonly PlatformMover _platformMover;
        private readonly Vector3 _startBallPosition;

        [Inject]
        public OnPlatformState(
            Lazy<IStateMachine> stateMachine,
            LevelInput input,
            Transform ballTransform,
            PlatformMover platformMover
        ) : base(stateMachine, input, ballTransform)
        {
            _platformMover = platformMover;
            _startBallPosition = new Vector3(
                _platformMover.PlatformPosition.x,
                BallTransform.position.y,
                BallTransform.position.z
            );
        }

        protected override void OnEnter()
        {
            BallTransform.position = new Vector3(
                _platformMover.PlatformPosition.x,
                _startBallPosition.y,
                _startBallPosition.z
            );

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
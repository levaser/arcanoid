using System;
using UnityEngine;
using Utility.StateSystem;
using VContainer;

namespace Game.Levels
{
    public abstract class BallState : State
    {
        protected readonly LevelInput Input;
        protected readonly Transform BallTransform;

        public BallState(
            Lazy<IStateMachine> stateMachine,
            LevelInput input,
            Transform ballTransform
        ) : base(stateMachine)
        {
            Input = input;
            BallTransform = ballTransform;
        }
    }
}
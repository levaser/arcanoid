using System;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelInput : IStartable, IDisposable
    {
        private readonly Controls _controls;
        private readonly BallMover _ball;

        [Inject]
        public LevelInput(
            Controls controls,
            BallMover ball
        )
        {
            _controls = controls;
            _ball = ball;
        }

        void IStartable.Start()
        {
            _controls.Level.Enable();

            _controls.Level.StandardInteraction.performed += OnStandardInteraction;
        }

        void IDisposable.Dispose()
        {
            _controls.Level.Disable();

            _controls.Level.StandardInteraction.performed -= OnStandardInteraction;
        }

        private void OnStandardInteraction(InputAction.CallbackContext context) => _ball.StartMoving();
    }
}
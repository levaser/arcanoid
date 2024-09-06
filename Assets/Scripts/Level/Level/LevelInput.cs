using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelInput : IStartable, IDisposable
    {
        private const float MouseInputMultiplier = 0.05f;

        private readonly Controls _controls;
        private readonly BallMover _ball;
        private readonly PlatformMover _platform;

        [Inject]
        public LevelInput(
            Controls controls,
            BallMover ball,
            PlatformMover platform
        )
        {
            _controls = controls;
            _ball = ball;
            _platform = platform;
        }

        void IStartable.Start()
        {
            _controls.Level.Enable();

            _controls.Level.StandardInteraction.performed += OnStandardInteraction;
            _controls.Level.Move.performed += OnMove;
        }

        void IDisposable.Dispose()
        {
            _controls.Level.Disable();

            _controls.Level.StandardInteraction.performed -= OnStandardInteraction;
            _controls.Level.Move.performed -= OnMove;
        }

        private void OnStandardInteraction(InputAction.CallbackContext context) => _ball.StartMoving();

        private void OnMove(InputAction.CallbackContext context) => _platform.Move(context.ReadValue<Vector2>().x * MouseInputMultiplier);
    }
}
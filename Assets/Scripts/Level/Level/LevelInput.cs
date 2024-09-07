using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelInput : IStartable, IDisposable
    {
        public event Action LevelStartPerformed;
        public event Action<float> PlatformMovePerformed;

        private const float MouseInputMultiplier = 0.05f;

        private readonly Controls _controls;

        [Inject]
        public LevelInput(
            Controls controls
        )
        {
            _controls = controls;
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

        private void OnStandardInteraction(InputAction.CallbackContext context) => LevelStartPerformed?.Invoke();

        private void OnMove(InputAction.CallbackContext context) => PlatformMovePerformed?.Invoke(context.ReadValue<Vector2>().x * MouseInputMultiplier);
    }
}
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class GameModeSwitcher : IStartable, IDisposable
    {
        private readonly Controls _controls;

        [Inject]
        public GameModeSwitcher(
            Controls controls
        )
        {
            _controls = controls;
        }

        void IStartable.Start()
        {
            SetPlayMode();
        }

        void IDisposable.Dispose()
        {
            SetMenuMode();
        }

        public void SetPlayMode()
        {
            _controls.Level.Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = 1;
        }

        public void SetMenuMode()
        {
            _controls.Level.Disable();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0;
        }
    }
}
using System;
using VContainer;
using VContainer.Unity;

namespace Game.MainMenu
{
    public sealed class InputController : IStartable, IDisposable
    {
        private readonly Controls _controls;

        [Inject]
        public InputController(
            Controls controls
        )
        {
            _controls = controls ?? throw new ArgumentNullException("controls is null");
        }

        void IStartable.Start()
        {
            _controls.Menu.Enable();
        }

        void IDisposable.Dispose()
        {
            _controls.Menu.Disable();
        }
    }
}
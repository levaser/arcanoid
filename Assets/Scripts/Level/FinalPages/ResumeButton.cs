using System;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class ResumeButton : IStartable, IDisposable
    {
        private readonly Button _button;
        private readonly PageSwitcher _pageSwitcher;

        [Inject]
        public ResumeButton(
            Button button,
            PageSwitcher pageSwitcher
        )
        {
            _button = button;
            _pageSwitcher = pageSwitcher;
        }

        void IStartable.Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _pageSwitcher.OnUnpause();
        }
    }
}
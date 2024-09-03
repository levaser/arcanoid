using System;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.MainMenu
{
    public sealed class FinishApplicationButton : IStartable, IDisposable
    {
        private readonly Button _button;
        private readonly ApplicationFinisher _applicationFinisher;

        [Inject]
        public FinishApplicationButton(
            Button button,
            ApplicationFinisher applicationFinisher
        )
        {
            _button = button;
            _applicationFinisher = applicationFinisher;
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
            _applicationFinisher.Finish();
        }
    }
}
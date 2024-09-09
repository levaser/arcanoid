using System;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class RestartLevelButton : IStartable, IDisposable
    {
        private readonly Button _button;
        private readonly LevelStarter _levelStarter;

        [Inject]
        public RestartLevelButton(
            Button button,
            LevelStarter levelStarter
        )
        {
            _button = button;
            _levelStarter = levelStarter;
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
            _levelStarter.Start(_levelStarter.CurrentLevelNumber);
        }
    }
}
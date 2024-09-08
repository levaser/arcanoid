using System;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class NextLevelButton : IStartable, IDisposable
    {
        private readonly Button _button;
        private readonly LevelStarter _levelStarter;

        [Inject]
        public NextLevelButton(
            Button button,
            LevelStarter levelStarter
        )
        {
            _button = button;
            _levelStarter = levelStarter;
        }

        void IStartable.Start()
        {
            if (_levelStarter.CurrentLevelNumber == _levelStarter.Levels.Length)
            {
                _button.gameObject.SetActive(false);
            }

            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _levelStarter.Start(_levelStarter.CurrentLevelNumber + 1);
        }
    }
}
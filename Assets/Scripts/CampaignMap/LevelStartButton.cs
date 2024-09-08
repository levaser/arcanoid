using System;
using Game.Levels;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.CampaignMap
{
    public sealed class LevelStartButton : IStartable, IDisposable
    {
        private readonly Button _button;
        private readonly int _targetLevelNumber;
        private readonly LevelStarter _levelStarter;

        [Inject]
        public LevelStartButton(
            Button button,
            int targetLevelNumber,
            LevelStarter levelStarter
        )
        {
            _button = button;
            _targetLevelNumber = targetLevelNumber;
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
            _levelStarter.Start(_targetLevelNumber);
        }
    }
}
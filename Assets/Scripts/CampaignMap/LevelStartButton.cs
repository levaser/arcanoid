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
        private readonly LevelConfig _levelConfig;
        private readonly LevelStarter _levelStarter;

        [Inject]
        public LevelStartButton(
            Button button,
            LevelConfig levelConfig,
            LevelStarter levelStarter
        )
        {
            _button = button;
            _levelConfig = levelConfig ?? throw new ArgumentNullException("level config is null");
            _levelStarter = levelStarter ?? throw new ArgumentNullException("level starter is null");
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
            _levelStarter.Start(_levelConfig);
        }
    }
}
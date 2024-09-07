using System;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.MainMenu
{
    public sealed class StartGameButton : IStartable, IDisposable
    {
        private readonly Button _button;
        private readonly GameStarter _gameStarter;

        [Inject]
        public StartGameButton(
            Button button,
            GameStarter gameStarter
        )
        {
            _button = button;
            _gameStarter = gameStarter;
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
            _gameStarter.LoadCampaignMap();
        }
    }
}
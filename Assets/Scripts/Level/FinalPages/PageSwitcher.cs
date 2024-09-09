using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class PageSwitcher : IStartable, IDisposable
    {
        private readonly LevelStats _levelStats;
        private readonly LevelInput _levelInput;
        private readonly GameModeSwitcher _gameModeSwitcher;
        private readonly GameObject _winPage;
        private readonly GameObject _losePage;
        private readonly GameObject _pausePage;

        [Inject]
        public PageSwitcher(
            LevelStats levelStats,
            LevelInput levelInput,
            GameModeSwitcher gameModeSwitcher,
            GameObject winPage,
            GameObject losePage,
            GameObject pausePage
        )
        {
            _levelStats = levelStats;
            _levelInput = levelInput;
            _gameModeSwitcher = gameModeSwitcher;
            _winPage = winPage;
            _losePage = losePage;
            _pausePage = pausePage;
        }

        void IStartable.Start()
        {
            _levelStats.Win += OnWin;
            _levelStats.Lose += OnLose;
            _levelInput.EscapePerformed += OnPause;
        }

        void IDisposable.Dispose()
        {
            _levelStats.Win -= OnWin;
            _levelStats.Lose -= OnLose;
            _levelInput.EscapePerformed -= OnPause;
        }

        private void OnWin()
        {
            _gameModeSwitcher.SetMenuMode();
            _winPage.SetActive(true);
        }
        private void OnLose()
        {
            _gameModeSwitcher.SetMenuMode();
            _losePage.SetActive(true);
        }

        private void OnPause()
        {
            _gameModeSwitcher.SetMenuMode();
            _pausePage.SetActive(true);
        }

        public void OnUnpause()
        {
            _gameModeSwitcher.SetMenuMode();
            _pausePage.SetActive(false);
        }
    }
}
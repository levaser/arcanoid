using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class PageSwitcher : IStartable, IDisposable
    {
        private readonly LevelStats _levelStats;
        private readonly LevelInput _levelInput;
        private readonly GameObject _winPage;
        private readonly GameObject _losePage;
        private readonly GameObject _pausePage;

        [Inject]
        public PageSwitcher(
            LevelStats levelStats,
            LevelInput levelInput,
            GameObject winPage,
            GameObject losePage,
            GameObject pausePage
        )
        {
            _levelStats = levelStats;
            _levelInput = levelInput;
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
            SwitchInput();
            _winPage.SetActive(true);
        }
        private void OnLose()
        {
            SwitchInput();
            _losePage.SetActive(true);
        }

        private void OnPause()
        {
            SwitchInput();
            Time.timeScale = 0;
            _pausePage.SetActive(true);
        }

        public void OnUnpause()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _levelInput.SetActiveLevelInput(true);
            _pausePage.SetActive(false);

            Time.timeScale = 1;
        }

        private void SwitchInput()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _levelInput.SetActiveLevelInput(false);
        }
    }
}
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class PageSwitcher : IStartable, IDisposable
    {
        private readonly GameObject _winPage;
        private readonly GameObject _losePage;
        private readonly LevelStats _levelStats;
        private readonly Controls _controls;

        [Inject]
        public PageSwitcher(
            LevelStats levelStats,
            Controls controls,
            GameObject winPage,
            GameObject losePage
        )
        {
            _levelStats = levelStats;
            _controls = controls;
            _winPage = winPage;
            _losePage = losePage;
        }

        public void Start()
        {
            _levelStats.Win += OnWin;
            _levelStats.Lose += OnLose;
        }

        public void Dispose()
        {
            _levelStats.Win -= OnWin;
            _levelStats.Lose -= OnLose;
        }

        private void OnWin()
        {
            ShowCursor();
            _controls.Level.Disable();
            _winPage.SetActive(true);
        }
        private void OnLose()
        {
            ShowCursor();
            _controls.Level.Disable();
            _losePage.SetActive(true);
        }

        private void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
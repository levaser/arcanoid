using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelFinisher : IStartable, IDisposable
    {
        private readonly LevelStats _levelStats;

        public LevelFinisher(
            LevelStats levelStats
        )
        {
            _levelStats = levelStats;
        }

        void IStartable.Start()
        {
            _levelStats.Win += OnWon;
            _levelStats.Lose += OnLost;
        }

        void IDisposable.Dispose()
        {
            _levelStats.Win -= OnWon;
            _levelStats.Lose -= OnLost;
        }

        public void OnWon()
        {
            ShowCursor();
            SceneManager.LoadScene("WinMenu");
        }

        public void OnLost()
        {
            ShowCursor();
            SceneManager.LoadScene("LoseMenu");
        }

        private void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
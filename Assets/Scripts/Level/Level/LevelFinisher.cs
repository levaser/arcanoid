using System;
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
            _levelStats.Won += OnWon;
            _levelStats.Lost += OnLost;
        }

        void IDisposable.Dispose()
        {
            _levelStats.Won -= OnWon;
            _levelStats.Lost -= OnLost;
        }

        public void OnWon()
        {
            SceneManager.LoadScene("WinMenu");
        }

        public void OnLost()
        {
            SceneManager.LoadScene("LoseMenu");
        }
    }
}
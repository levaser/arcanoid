using System;
using Game.Levels;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class LevelStarter
    {
        private readonly LevelConfig[] _levels;

        public int CurrentLevelNumber { get; private set; }
        public LevelConfig CurrentLevelConfig { get; private set; }

        public LevelStarter(
            LevelConfig[] levels
        )
        {
            _levels = levels;
        }

        public void Start(int levelNumber)
        {
            if (_levels.Length <= levelNumber)
                throw new ArgumentOutOfRangeException(nameof(levelNumber), "Parameter 'levelNumber' out of range of '_levels'");

            CurrentLevelNumber = levelNumber;
            CurrentLevelConfig = _levels[levelNumber];

            SceneManager.LoadScene("Level");
        }
    }
}
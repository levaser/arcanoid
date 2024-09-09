using System;
using Game.Levels;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class LevelStarter
    {
        public event Action LevelStarted;

        public readonly LevelConfig[] Levels;

        public int CurrentLevelNumber { get; private set; }
        public LevelConfig CurrentLevelConfig { get; private set; }

        public LevelStarter(
            LevelConfig[] levels
        )
        {
            Levels = levels;
        }

        public void Start(int levelNumber)
        {
            if (Levels.Length <= levelNumber)
                throw new ArgumentOutOfRangeException(nameof(levelNumber), "Parameter 'levelNumber' out of range of '_levels'");

            CurrentLevelNumber = levelNumber;
            CurrentLevelConfig = Levels[levelNumber];

            SceneManager.LoadScene("Level");
        }
    }
}
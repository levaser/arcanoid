using System;
using Game.Levels;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class LevelStarter
    {
        public LevelConfig LevelConfig { get; private set; }

        public void Start(LevelConfig levelConfig)
        {
            LevelConfig = levelConfig;
            SceneManager.LoadScene("Level");
        }
    }
}
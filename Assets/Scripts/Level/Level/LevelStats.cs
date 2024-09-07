using System;

namespace Game.Levels
{
    public sealed class LevelStats
    {
        public event Action Won;
        public event Action Lost;

        public int Score = 0;

        private int _hp = 1;
        public int HP
        {
            get => _hp;
            set
            {
                _hp = value;
                if (_hp <= 0)
                {
                    Lost?.Invoke();
                }
            }
        }

        private int _enemiesNumber = 0;
        public int EnemiesNumber
        {
            get => _enemiesNumber;
            set
            {
                _enemiesNumber = value;
                if (_enemiesNumber <= 0)
                {
                    Won?.Invoke();
                }
            }
        }
    }
}
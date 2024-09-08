using System;

namespace Game.Levels
{
    public sealed class LevelStats
    {
        public event Action Won;
        public event Action Lost;

        private int _score = 0;
        public int Score
        {
            get => _score;
            set
            {
                ScoreChanged?.Invoke(value);
                _score = value;
            }
        }
        public event Action<int> ScoreChanged;

        private int _hp = 1;
        public int HP
        {
            get => _hp;
            set
            {
                if (_hp > value)
                    HPDecreased?.Invoke();
                else
                    HPIncreased?.Invoke();

                _hp = value;
                if (_hp <= 0)
                {
                    Lost?.Invoke();
                }
            }
        }
        public event Action HPDecreased;
        public event Action HPIncreased;

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
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelLoader : IStartable
    {
        private readonly LevelConfig _levelConfig;
        private readonly Transform _gridTransform;
        private readonly GameObject _enemyPrefab;
        private readonly LevelStats _levelStats;

        [Inject]
        public LevelLoader(
            LevelStarter levelStarter,
            Transform gridTransform,
            GameObject enemyPrefab,
            LevelStats levelStats
        )
        {
            if (levelStarter == null)
                throw new System.ArgumentNullException(nameof(LevelStarter), "Parameter 'levelStarter' cannot be null");

            _levelConfig = levelStarter.LevelConfig
                ?? throw new System.ArgumentNullException(nameof(levelStarter.LevelConfig), "Parameter 'levelConfig' cannot be null");

            _gridTransform = gridTransform
                ?? throw new System.ArgumentNullException(nameof(gridTransform), "Parameter 'gridTransform' cannot be null");

            _enemyPrefab = enemyPrefab
                ?? throw new System.ArgumentNullException(nameof(enemyPrefab), "Parameter 'enemyPrefab' cannot be null");

            _levelStats = levelStats;
        }

        void IStartable.Start()
        {
            for (int row = 0; row < _levelConfig.Grid.GridSize.y; row++)
            {
                for (int column = 0; column < _levelConfig.Grid.GridSize.x; column++)
                {
                    if (_levelConfig.Grid.GetCell(column, row) == 1)
                    {
                        GameObject enemy = Object.Instantiate(_enemyPrefab, new Vector3(column, -row * 0.75f, 0) + _gridTransform.localPosition, Quaternion.identity, _gridTransform);
                        enemy.GetComponent<SpriteRenderer>().sortingOrder = row;
                        _levelStats.EnemiesNumber++;
                    }
                }
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
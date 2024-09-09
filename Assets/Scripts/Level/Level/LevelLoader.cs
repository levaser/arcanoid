using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelLoader : IStartable
    {
        private readonly LevelConfig _levelConfig;
        private readonly Transform _gridTransform;
        private readonly GameObject _defaultEnemyPrefab;
        private readonly GameObject _forcedEnemyPrefab;
        private readonly LevelStats _levelStats;

        [Inject]
        public LevelLoader(
            LevelStarter levelStarter,
            Transform gridTransform,
            GameObject defaultEnemyPrefab,
            GameObject forcedEnemyPrefab,
            LevelStats levelStats
        )
        {
            if (levelStarter == null)
                throw new System.ArgumentNullException(nameof(LevelStarter), "Parameter 'levelStarter' cannot be null");

            _levelConfig = levelStarter.CurrentLevelConfig
                ?? throw new System.ArgumentNullException(nameof(levelStarter.CurrentLevelConfig), "Parameter 'levelConfig' cannot be null");

            _gridTransform = gridTransform
                ?? throw new System.ArgumentNullException(nameof(gridTransform), "Parameter 'gridTransform' cannot be null");

            _defaultEnemyPrefab = defaultEnemyPrefab
                ?? throw new System.ArgumentNullException(nameof(defaultEnemyPrefab), "Parameter 'enemyPrefab' cannot be null");
            _forcedEnemyPrefab = forcedEnemyPrefab;

            _levelStats = levelStats;
        }

        void IStartable.Start()
        {
            BuildLevel();
        }

        private void BuildLevel()
        {
            for (int row = 0; row < _levelConfig.Grid.GridSize.y; row++)
            {
                for (int column = 0; column < _levelConfig.Grid.GridSize.x; column++)
                {
                    if (_levelConfig.Grid.GetCell(column, row) == true)
                    {
                        GameObject enemy = Object.Instantiate(
                            Random.Range(0, 10) == 0 ? _forcedEnemyPrefab : _defaultEnemyPrefab,
                            new Vector3(column, -row * 0.75f, 0) + _gridTransform.localPosition,
                            Quaternion.identity,
                            _gridTransform
                        );
                        enemy.GetComponent<SpriteRenderer>().sortingOrder = row;
                        _levelStats.EnemiesNumber++;
                    }
                }
            }
        }
    }
}
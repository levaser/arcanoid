using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class HPView : IStartable, IDisposable
    {
        private readonly LevelStats _levelStats;
        private readonly Transform _rootTransform;
        private readonly GameObject _prefab;

        private Stack<GameObject> _hpInstances;

        [Inject]
        public HPView(
            LevelStats levelStats,
            Transform rootTransform,
            GameObject prefab
        )
        {
            _levelStats = levelStats;
            _rootTransform = rootTransform;
            _prefab = prefab;

            _hpInstances = new Stack<GameObject>();
        }

        void IStartable.Start()
        {
            for (int i = 0; i < _levelStats.HP; i++)
            {
                OnHPIncreased();
            }

            _levelStats.HPIncreased += OnHPIncreased;
            _levelStats.HPDecreased += OnHPDecreased;
        }

        void IDisposable.Dispose()
        {
            _levelStats.HPIncreased -= OnHPIncreased;
            _levelStats.HPDecreased -= OnHPDecreased;
        }

        private void OnHPIncreased()
        {
            _hpInstances.Push(UnityEngine.Object.Instantiate(_prefab, _rootTransform));
        }

        private void OnHPDecreased()
        {
            UnityEngine.Object.Destroy(_hpInstances.Pop());
        }
    }
}
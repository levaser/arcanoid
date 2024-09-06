using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelInstaller : LifetimeScope
    {
        [SerializeField]
        private Transform _enemyGridTransform;

        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private BallConfig _ballConfig;

        [SerializeField]
        private Transform _ballTransform;

        [SerializeField]
        private Transform _platformTransform;

        protected override void Configure(IContainerBuilder builder)
        {
            // builder.RegisterInstance(_enemyGridTransform);
            // builder.RegisterInstance(_enemyPrefab);

            builder.RegisterEntryPoint<LevelLoader>(Lifetime.Scoped)
                .WithParameter(_enemyGridTransform)
                .WithParameter(_enemyPrefab);

            ConfigureBall(builder);

            builder.RegisterEntryPoint<LevelInput>(Lifetime.Scoped);
        }

        private void ConfigureBall(IContainerBuilder builder)
        {
            // builder.RegisterInstance(_ballConfig);
            // builder.RegisterInstance(_ballTransform);

            builder.RegisterEntryPoint<BallMover>(Lifetime.Scoped)
                .WithParameter(_ballTransform)
                .WithParameter(_ballConfig)
                .AsSelf();
        }
    }
}
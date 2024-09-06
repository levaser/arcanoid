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

        [SerializeField]
        private PlatformConfig _platformConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelLoader>(Lifetime.Scoped)
                .WithParameter(_enemyGridTransform)
                .WithParameter(_enemyPrefab);

            ConfigureBall(builder);
            ConfigurePlatform(builder);

            builder.RegisterEntryPoint<LevelInput>(Lifetime.Scoped);
        }

        private void ConfigureBall(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BallMover>(Lifetime.Scoped)
                .WithParameter(_ballTransform)
                .WithParameter(_ballConfig)
                .AsSelf();
        }

        private void ConfigurePlatform(IContainerBuilder builder)
        {
            builder.Register<PlatformMover>(Lifetime.Scoped)
                .WithParameter(_platformTransform)
                .WithParameter(_platformConfig);
        }
    }
}
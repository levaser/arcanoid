using System;
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
            builder.Register<LevelStats>(Lifetime.Scoped);
            builder.RegisterEntryPoint<LevelLoader>(Lifetime.Scoped)
                .WithParameter(_enemyGridTransform)
                .WithParameter(_enemyPrefab);
            builder.RegisterEntryPoint<LevelFinisher>(Lifetime.Scoped);

            builder.RegisterEntryPoint<LevelInput>(Lifetime.Scoped).AsSelf();

            ConfigurePlatform(builder);
            ConfigureBall(builder);
        }

        private void ConfigureBall(IContainerBuilder builder)
        {
            builder.Register<BallState, OnPlatformState>(Lifetime.Scoped)
                .WithParameter("ballTransform", _ballTransform)
                .WithParameter("platformTransform", _platformTransform);
            builder.Register<BallState, AttackState>(Lifetime.Scoped)
                .WithParameter(_ballTransform)
                .WithParameter(_ballConfig);

            builder.RegisterEntryPoint<BallStateMachine>(Lifetime.Scoped)
                .As<Utility.StateSystem.IStateMachine, BallStateMachine>();

            builder.Register(container => new Lazy<Utility.StateSystem.IStateMachine>(() => container.Resolve<Utility.StateSystem.IStateMachine>()), Lifetime.Scoped);
        }

        private void ConfigurePlatform(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlatformMover>(Lifetime.Scoped)
                .WithParameter(_platformTransform)
                .WithParameter(_platformConfig)
                .AsSelf();
        }
    }
}
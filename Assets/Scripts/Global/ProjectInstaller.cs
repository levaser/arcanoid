using Game.Levels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public sealed class ProjectInstaller : LifetimeScope
    {
        [SerializeField]
        private ApplicationFinisher _applicationFinisher;

        [SerializeField]
        private LevelConfig[] _levels;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Controls>(Lifetime.Singleton);
            builder.Register<GameStarter>(Lifetime.Singleton);
            builder.RegisterInstance(_applicationFinisher);
            builder.Register<LevelStarter>(Lifetime.Singleton)
                .WithParameter(_levels);
        }
    }
}

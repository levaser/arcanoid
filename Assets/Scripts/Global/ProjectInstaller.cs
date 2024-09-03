using VContainer;
using VContainer.Unity;

namespace Game
{
    public sealed class ProjectInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Controls>(Lifetime.Singleton);
            builder.Register<GameStarter>(Lifetime.Singleton);
            builder.Register<ApplicationFinisher>(Lifetime.Singleton);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.MainMenu
{
    public sealed class MainMenuInstaller : LifetimeScope
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _finishButton;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputController>(Lifetime.Scoped);
            builder.RegisterEntryPoint<StartGameButton>(Lifetime.Scoped).WithParameter(_startButton);
            builder.RegisterEntryPoint<FinishApplicationButton>(Lifetime.Scoped).WithParameter(_finishButton);
        }
    }
}

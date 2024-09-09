using Game.FinalMenues;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.CampaignMap
{
    public sealed class CampaignMapInstaller : LifetimeScope
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button[] _levelStartButtons;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BackButton>(Lifetime.Scoped)
                .WithParameter(_backButton);

            for (int i = 0; i < _levelStartButtons.Length; i++)
            {
                builder.RegisterEntryPoint<LevelStartButton>(Lifetime.Scoped)
                    .WithParameter(_levelStartButtons[i])
                    .WithParameter(i);
            }
        }
    }
}
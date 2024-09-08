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
        private ButtonToConfigPair[] _buttonToConfigPairs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BackButton>(Lifetime.Scoped)
                .WithParameter(_backButton);

            foreach (var e in _buttonToConfigPairs)
            {
                builder.RegisterEntryPoint<LevelStartButton>(Lifetime.Scoped)
                    .WithParameter(e.Button)
                    .WithParameter(e.LevelConfig);
            }
        }
    }
}
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.CampaignMap
{
    public sealed class CampaignMapInstaller : LifetimeScope
    {
        [SerializeField]
        private ButtonToConfigPair[] _buttonToConfigPairs;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var e in _buttonToConfigPairs)
            {
                builder.RegisterEntryPoint<LevelStartButton>(Lifetime.Scoped)
                    .WithParameter(e.Button)
                    .WithParameter(e.LevelConfig);
            }
        }
    }
}
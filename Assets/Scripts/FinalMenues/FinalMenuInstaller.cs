using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.FinalMenues
{
    public class FinalMenuInstaller : LifetimeScope
    {
        [SerializeField]
        private Button _toCampaignButton;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ToCampaignButton>(Lifetime.Scoped)
                .WithParameter(_toCampaignButton);
        }
    }
}
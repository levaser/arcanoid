using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelInstaller : LifetimeScope
    {
        [Header("Enemy")]
        [SerializeField] private Transform _enemyGridTransform;
        [SerializeField] private GameObject _defaultEnemyPrefab;
        [SerializeField] private GameObject _forcedEnemyPrefab;


        [Header("Ball")]
        [SerializeField] private BallConfig _ballConfig;
        [SerializeField] private Transform _ballTransform;
        [SerializeField] private AudioSource _collisionAudioSource;


        [Header("Platform")]
        [SerializeField] private PlatformConfig _platformConfig;
        [SerializeField] private Transform _platformTransform;


        [Header("HP View")]
        [SerializeField] private Transform _hpRootTransform;
        [SerializeField] private GameObject _hpPrefab;


        [Header("Score View")]
        [SerializeField] private TMP_Text _scoreText;


        [Header("Pages")]
        [SerializeField] private GameObject _winPage;
        [SerializeField] private GameObject _losePage;
        [SerializeField] private GameObject _pausePage;
        [SerializeField] private Button[] _toCampaignButtons;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _unpauseButton;
        [SerializeField] private AudioSource _winAudio;
        [SerializeField] private AudioSource _loseAudio;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelStats>(Lifetime.Scoped);
            builder.RegisterEntryPoint<LevelLoader>(Lifetime.Scoped)
                .WithParameter(_enemyGridTransform)
                .WithParameter("defaultEnemyPrefab", _defaultEnemyPrefab)
                .WithParameter("forcedEnemyPrefab", _forcedEnemyPrefab);

            builder.RegisterEntryPoint<LevelInput>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<GameModeSwitcher>(Lifetime.Scoped).AsSelf();

            ConfigurePlatform(builder);
            ConfigureBall(builder);

            ConfigureHPView(builder);
            ConfigureScoreView(builder);

            ConfigurePages(builder);
        }

        private void ConfigureBall(IContainerBuilder builder)
        {
            builder.Register<BallState, OnPlatformState>(Lifetime.Scoped)
                .WithParameter("ballTransform", _ballTransform)
                .WithParameter("platformTransform", _platformTransform);
            builder.Register<BallState, AttackState>(Lifetime.Scoped)
                .WithParameter(_ballTransform)
                .WithParameter(_ballConfig)
                .WithParameter(_collisionAudioSource);

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

        private void ConfigureHPView(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<HPView>(Lifetime.Scoped)
                .WithParameter(_hpRootTransform)
                .WithParameter(_hpPrefab);
        }

        private void ConfigureScoreView(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScoreView>(Lifetime.Scoped)
                .WithParameter(_scoreText);
        }

        private void ConfigurePages(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PageSwitcher>(Lifetime.Scoped)
                .WithParameter("winPage", _winPage)
                .WithParameter("losePage", _losePage)
                .WithParameter("pausePage", _pausePage)
                .WithParameter("winAudio", _winAudio)
                .WithParameter("loseAudio", _loseAudio)
                .AsSelf();

            foreach (var e in _toCampaignButtons)
                builder.RegisterEntryPoint<ToCampaignButton>(Lifetime.Scoped)
                    .WithParameter(e);

            builder.RegisterEntryPoint<NextLevelButton>(Lifetime.Scoped)
                .WithParameter(_nextLevelButton);

            builder.RegisterEntryPoint<RestartLevelButton>(Lifetime.Scoped)
                .WithParameter(_restartLevelButton);

            builder.RegisterEntryPoint<ResumeButton>(Lifetime.Scoped)
                .WithParameter(_unpauseButton);
        }
    }
}
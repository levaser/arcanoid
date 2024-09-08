using System;
using TMPro;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class ScoreView : IStartable, IDisposable
    {
        private readonly LevelStats _levelStats;
        private readonly TMP_Text _text;

        [Inject]
        public ScoreView(
            LevelStats levelStats,
            TMP_Text text
        )
        {
            _levelStats = levelStats;
            _text = text;
        }

        void IStartable.Start()
        {
            _levelStats.ScoreChanged += OnScoreChanged;
        }

        void IDisposable.Dispose()
        {
            _levelStats.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int newScore)
        {
            _text.text = newScore.ToString();
        }
    }
}
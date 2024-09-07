using System;
using Game.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CampaignMap
{
    [Serializable]
    public sealed class ButtonToConfigPair
    {
        [field: SerializeField]
        public Button Button { get; private set; }

        [field: SerializeField]
        public LevelConfig LevelConfig { get; private set; }
    }
}
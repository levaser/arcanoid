using Array2DEditor;
using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "New Level Config", menuName = "Level Config", order = 51)]
    public sealed class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public Array2DBool Grid { get; private set; }
    }
}
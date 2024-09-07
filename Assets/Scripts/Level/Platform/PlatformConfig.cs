using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "New Platform Config", menuName = "Plarform Config", order = 51)]
    public sealed class PlatformConfig : ScriptableObject
    {
        [field: SerializeField]
        public float Speed { get; private set; } = 1f;

        [field: SerializeField]
        public float LeftMoveLimit { get; private set; } = -5f;

        [field: SerializeField]
        public float RightMoveLimit { get; private set; } = 3f;
    }
}
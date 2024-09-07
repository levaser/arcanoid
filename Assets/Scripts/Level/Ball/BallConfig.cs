using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "New Ball Config", menuName = "Ball Config", order = 51)]
    public sealed class BallConfig : ScriptableObject
    {
        [field: SerializeField]
        public float Speed { get; private set; } = 3f;

        [field: SerializeField]
        public float MaxSpeed { get; private set; } = 8f;

        [field: SerializeField]
        public float Radius { get; private set; } = 0.42f;
    }
}
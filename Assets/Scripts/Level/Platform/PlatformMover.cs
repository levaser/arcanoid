using UnityEngine;
using VContainer;

namespace Game.Levels
{
    public sealed class PlatformMover
    {
        private readonly Transform _transform;

        [Inject]
        public PlatformMover(
            Transform transform
        )
        {
            _transform = transform;
        }

        public void Move(float velocity)
        {}
    }
}
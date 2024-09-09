using System;
using UnityEngine;
using VContainer;

namespace Game.Levels
{
    public sealed class BallCollisionChecker
    {
        public event Action CollisionDetected;

        private readonly Transform _transform;
        private readonly BallConfig _config;

        private RaycastHit2D _hit;
        public RaycastHit2D[] Hits { get; private set; }
        public int HitsNumber { get; private set; }

        [Inject]
        public BallCollisionChecker(
            Transform transform,
            BallConfig config
        )
        {
            _transform = transform;
            _config = config;

            Hits = new RaycastHit2D[10];
        }

        public void CheckCollisionsInDirection(Vector2 velocity)
        {
            HitsNumber = Physics2D.CircleCastNonAlloc(
                _transform.position + new Vector3(0f, _config.Radius / 2, 0f),
                _config.Radius / 2,
                velocity,
                Hits,
                velocity.magnitude * Time.fixedDeltaTime + 0.02f,
                LayerMask.GetMask("Default", "Platform")
            );

            if (HitsNumber > 0)
                CollisionDetected?.Invoke();
        }
    }
}
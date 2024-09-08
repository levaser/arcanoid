using System;
using UnityEngine;
using VContainer;

namespace Game.Levels
{
    public sealed class BallCollisionChecker
    {
        public event Action<RaycastHit2D> CollisionDetected;

        private readonly Transform _transform;
        private readonly BallConfig _config;

        private RaycastHit2D _hit;

        [Inject]
        public BallCollisionChecker(
            Transform transform,
            BallConfig config
        )
        {
            _transform = transform;
            _config = config;
        }

        public void CheckCollisionsInDirection(Vector2 velocity)
        {
            _hit = Physics2D.CircleCast(
                _transform.position + new Vector3(0f, _config.Radius / 2, 0f),
                _config.Radius / 2,
                velocity,
                velocity.magnitude * Time.fixedDeltaTime,
                LayerMask.GetMask("Default", "Platform")
            );

            if (_hit.collider != null)
                CollisionDetected?.Invoke(_hit);
        }
    }
}
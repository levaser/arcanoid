using System;
using UnityEditor.Rendering;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class BallMover : IStartable, IDisposable, IFixedTickable
    {
        private readonly Transform _transform;
        private readonly BallConfig _config;
        private readonly BallCollisionChecker _collisionChecker;
        private readonly Rigidbody2D _rigibody;

        private Vector2 _moveDirection = Vector2.zero;
        private float _speed = 0f;

        [Inject]
        public BallMover(
            Transform transform,
            BallConfig config
        )
        {
            _transform = transform;
            _config = config;
            _rigibody = _transform.GetComponent<Rigidbody2D>();

            _collisionChecker = new BallCollisionChecker(_transform, _config);
        }

        void IStartable.Start()
        {
            _collisionChecker.CollisionDetected += OnCollisionDetected;
        }

        void IDisposable.Dispose()
        {
            _collisionChecker.CollisionDetected -= OnCollisionDetected;
        }

        public void StartMoving()
        {
            if (_moveDirection == Vector2.zero)
            {
                _moveDirection = Vector2.up;
                _speed = _config.Speed;

                _rigibody.velocity = _moveDirection * _speed;
            }
        }

        void IFixedTickable.FixedTick()
        {
            _collisionChecker.CheckCollisionsInDirection(_moveDirection);
            // Move();
        }

        private void OnCollisionDetected(RaycastHit2D hit)
        {
            if (hit.transform.GetComponent<MarkerClass>()
                .GetType().GetInterface(typeof(IDefaultReflectable).ToString()) != null)
            {
                _moveDirection = Vector2.Reflect(_moveDirection, hit.normal);
                _speed = Mathf.Clamp(_speed + 0.05f, 0f, _config.MaxSpeed);

                _rigibody.velocity = _moveDirection * _speed;
            }
        }

        // private void Move()
        // {
        //     // _transform.Translate((Vector3)_moveDirection * _speed * Time.fixedDeltaTime);
        //     _rigibody.velocity = _moveDirection * _speed;
        // }
    }
}
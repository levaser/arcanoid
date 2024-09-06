using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void OnCollisionDetected(RaycastHit2D hit)
        {
            MarkerClass target = hit.transform.GetComponent<MarkerClass>();
            List<Type> typeInterfaces = new List<Type>(hit.transform.GetComponent<MarkerClass>().GetType().GetInterfaces());
            if (typeInterfaces.Find(e => e == typeof(IEnemyReflectable)) != null)
            {
                ChangeMoveDirection(Vector2.Reflect(_moveDirection, hit.normal));
                (target as IEnemyReflectable).OnTouch();
            }
            else if (typeInterfaces.Find(e => e == typeof(ICustomRelfectable)) != null)
            {
                ChangeMoveDirection((hit.transform.GetComponent<MarkerClass>() as ICustomRelfectable).GetReflectedDirection(hit));
            }
            else if (typeInterfaces.Find(e => e == typeof(IDefaultReflectable)) != null)
            {
                ChangeMoveDirection(Vector2.Reflect(_moveDirection, hit.normal));
            }
        }

        private void ChangeMoveDirection(Vector2 newDirection)
        {
            _moveDirection = newDirection;
            _speed = Mathf.Clamp(_speed + 0.05f, 0f, _config.MaxSpeed);

            _rigibody.velocity = _moveDirection * _speed;
        }
    }
}
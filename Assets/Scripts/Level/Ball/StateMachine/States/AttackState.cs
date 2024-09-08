using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.StateSystem;
using VContainer;

namespace Game.Levels
{
    public sealed class AttackState : BallState
    {
        private readonly BallConfig _config;
        private readonly BallCollisionChecker _collisionChecker;
        private readonly Rigidbody2D _rigidbody;
        private readonly LevelStats _levelStats;

        private Vector2 _moveDirection;
        private float _speed;

        [Inject]
        public AttackState(
            Lazy<IStateMachine> stateMachine,
            LevelInput input,
            Transform ballTransform,
            BallConfig config,
            LevelStats levelStats
        ) : base(stateMachine, input, ballTransform)
        {
            _config = config;
            _collisionChecker = new BallCollisionChecker(BallTransform, _config);
            _rigidbody = BallTransform.GetComponent<Rigidbody2D>();
            _levelStats = levelStats;
        }

        protected override void OnEnter()
        {
            _moveDirection = Vector2.up;
            _speed = _config.Speed;
            _rigidbody.velocity = _moveDirection * _speed;

            _collisionChecker.CollisionDetected += OnCollisionDetected;
        }

        protected override void OnExit()
        {
            _collisionChecker.CollisionDetected -= OnCollisionDetected;
        }

        public override void Update()
        {
            _collisionChecker.CheckCollisionsInDirection(_moveDirection * _speed);
        }

        private void OnCollisionDetected(RaycastHit2D hit)
        {
            MarkerClass target = hit.transform.GetComponent<MarkerClass>();
            if (target is IEnemyReflectable enemy)
            {
                ChangeMoveDirection(Vector2.Reflect(_moveDirection, hit.normal));
                enemy.OnTouch();
                _levelStats.EnemiesNumber--;
            }
            else if (target is ICustomRelfectable platform)
            {
                ChangeMoveDirection(platform.GetReflectedDirection(hit));
            }
            else if (target is IDefaultReflectable)
            {
                ChangeMoveDirection(Vector2.Reflect(_moveDirection, hit.normal));
            }
            else if (target is IDeadReflectable)
            {
                _levelStats.HP--;
            }
        }

        private void ChangeMoveDirection(Vector2 newDirection)
        {
            _moveDirection = newDirection;
            _speed = Mathf.Clamp(_speed + 0.05f, 0f, _config.MaxSpeed);

            _rigidbody.velocity = _moveDirection * _speed;
        }
    }
}
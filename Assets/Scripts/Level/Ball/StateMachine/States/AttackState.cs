using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateSystem;
using VContainer;

namespace Game.Levels
{
    public sealed class AttackState : BallState
    {
        private readonly BallConfig _config;
        private readonly BallCollisionChecker _collisionChecker;
        private readonly Rigidbody2D _rigidbody;

        private Vector2 _moveDirection;
        private float _speed;

        [Inject]
        public AttackState(
            Lazy<IStateMachine> stateMachine,
            LevelInput input,
            Transform ballTransform,
            BallConfig config
        ) : base(stateMachine, input, ballTransform)
        {
            _config = config;
            _collisionChecker = new BallCollisionChecker(BallTransform, _config);
            _rigidbody = BallTransform.GetComponent<Rigidbody2D>();
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

            _rigidbody.velocity = _moveDirection * _speed;
        }
    }
}
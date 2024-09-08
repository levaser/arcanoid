using System;
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
            _levelStats.HPChanged += OnHPChanged;
        }

        protected override void OnExit()
        {
            _collisionChecker.CollisionDetected -= OnCollisionDetected;
            _levelStats.HPChanged -= OnHPChanged;
        }

        public override void Update()
        {
            _collisionChecker.CheckCollisionsInDirection(_moveDirection * _speed);
        }

        private void OnCollisionDetected(RaycastHit2D hit)
        {
            MarkerClass target = hit.transform.GetComponent<MarkerClass>();
            if (target is IReflectable reflectable)
            {
                ChangeMoveDirection(reflectable.GetReflectedDirection(_moveDirection, hit));
                reflectable.OnContactPerformed(_levelStats);
            }
        }

        private void ChangeMoveDirection(Vector2 newDirection)
        {
            _moveDirection = newDirection;
            _speed = Mathf.Clamp(_speed + 0.05f, 0f, _config.MaxSpeed);

            _rigidbody.velocity = _moveDirection * _speed;
        }

        private void OnHPChanged(int newHP)
        {
            StateMachine.SetState<OnPlatformState>();
        }
    }
}
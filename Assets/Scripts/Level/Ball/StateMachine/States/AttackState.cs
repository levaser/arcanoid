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
        private readonly AudioSource _audioSource;

        private Vector2 _moveDirection;
        private float _speed;

        [Inject]
        public AttackState(
            Lazy<IStateMachine> stateMachine,
            LevelInput input,
            Transform ballTransform,
            BallConfig config,
            LevelStats levelStats,
            AudioSource audioSource
        ) : base(stateMachine, input, ballTransform)
        {
            _config = config;
            _collisionChecker = new BallCollisionChecker(BallTransform, _config);
            _rigidbody = BallTransform.GetComponent<Rigidbody2D>();
            _levelStats = levelStats;
            _audioSource = audioSource;
        }

        protected override void OnEnter()
        {
            _moveDirection = Vector2.up;
            _speed = _config.Speed;
            _rigidbody.velocity = _moveDirection * _speed;

            _collisionChecker.CollisionDetected += OnCollisionDetected;
            _levelStats.HPDecreased += OnHPDecreased;
            _levelStats.Win += OnFinish;
            _levelStats.Win += OnFinish;
        }

        protected override void OnExit()
        {
            _collisionChecker.CollisionDetected -= OnCollisionDetected;
            _levelStats.HPDecreased -= OnHPDecreased;
            _levelStats.Win -= OnFinish;
            _levelStats.Win -= OnFinish;
        }

        public override void Update()
        {
            _collisionChecker.CheckCollisionsInDirection(_moveDirection * _speed);
        }

        private void OnCollisionDetected()
        {
            MarkerClass target = _collisionChecker.Hits[_collisionChecker.HitsNumber - 1].transform.GetComponent<MarkerClass>();
            if (target is IReflectable reflectable)
            {
                ChangeMoveDirection(reflectable.GetReflectedDirection(_moveDirection, _collisionChecker.Hits[_collisionChecker.HitsNumber - 1]));
                reflectable.OnContactPerformed(_levelStats);
                _audioSource.Play();
            }
        }

        private void ChangeMoveDirection(Vector2 newDirection)
        {
            _moveDirection = newDirection;
            _speed = Mathf.Clamp(_speed + 0.05f, 0f, _config.MaxSpeed);

            _rigidbody.velocity = _moveDirection * _speed;
        }

        private void OnHPDecreased()
        {
            StateMachine.SetState<OnPlatformState>();
        }

        private void OnFinish()
        {
            StateMachine.SetState<OnPlatformState>();
        }
    }
}
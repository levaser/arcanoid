using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class PlatformMover : IStartable, IDisposable
    {
        public event Action<float> PlatformMoved;

        private readonly Transform _transform;
        private readonly PlatformConfig _config;
        private readonly LevelInput _input;

        public Vector3 PlatformPosition => _transform.position;

        [Inject]
        public PlatformMover(
            Transform transform,
            PlatformConfig config,
            LevelInput input
        )
        {
            _transform = transform;
            _config = config;
            _input = input;
        }

        void IStartable.Start()
        {
            _input.PlatformMovePerformed += OnPlatformMovePreformed;
        }

        void IDisposable.Dispose()
        {
            _input.PlatformMovePerformed -= OnPlatformMovePreformed;
        }

        public void OnPlatformMovePreformed(float input)
        {
            _transform.localPosition = new Vector3(
                Mathf.Clamp(_transform.localPosition.x + input * _config.Speed, _config.LeftMoveLimit, _config.RightMoveLimit),
                _transform.localPosition.y,
                _transform.localPosition.z
            );

            PlatformMoved?.Invoke(Mathf.Clamp(_transform.localPosition.x + input * _config.Speed, _config.LeftMoveLimit, _config.RightMoveLimit));
        }
    }
}
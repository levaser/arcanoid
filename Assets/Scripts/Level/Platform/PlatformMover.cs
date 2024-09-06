using UnityEngine;
using VContainer;

namespace Game.Levels
{
    public sealed class PlatformMover
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly BoxCollider2D _collider;
        private readonly PlatformConfig _config;

        [Inject]
        public PlatformMover(
            Transform transform,
            PlatformConfig config
        )
        {
            _transform = transform;
            _rigidbody = transform.GetComponent<Rigidbody2D>();
            _collider = transform.GetComponent<BoxCollider2D>();
            _config = config;
        }

        public void Move(float input)
        {
            // if (Physics2D.Raycast(_transform.position, Vector2.right * Mathf.Sign(input), _collider.size.x / 2 + 0.1f, LayerMask.GetMask("Default")).collider == null)
            // if (_config.LeftMoveLimit < _transform.localPosition.x && _transform.localPosition.x < _config.RightMoveLimit)
            //     _transform.localPosition += input * _config.Speed * (Vector3)Vector2.right;
            _transform.localPosition = new Vector3(
                Mathf.Clamp(_transform.localPosition.x + input * _config.Speed, _config.LeftMoveLimit, _config.RightMoveLimit),
                _transform.localPosition.y,
                _transform.localPosition.z
            );
        }
    }
}
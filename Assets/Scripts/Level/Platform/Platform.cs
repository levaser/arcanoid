using UnityEngine;
using Utility;

namespace Game.Levels
{
    public sealed class Platform : MarkerClass, IReflectable
    {
        private const int maxAngle = 70;

        private BoxCollider2D _collider;

        public void Start()
        {
            _collider = transform.GetComponent<BoxCollider2D>();
        }

        public void OnContactPerformed(LevelStats levelStats) {}

        public Vector2 GetReflectedDirection(Vector2 direction, RaycastHit2D hit)
        {
            return Vector2.up.Rotate(-CalculateHitAngle(hit.point.x));
        }

        private float CalculateHitAngle(float hitX)
        {
            return Mathf.Clamp((hitX - transform.position.x) * maxAngle / (_collider.size.x / 2), -maxAngle, maxAngle);
        }
    }
}
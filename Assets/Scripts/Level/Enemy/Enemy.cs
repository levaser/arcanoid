using UnityEngine;

namespace Game.Levels
{
    public sealed class Enemy : MarkerClass, IReflectable
    {
        public Vector2 GetReflectedDirection(Vector2 direction, RaycastHit2D hit)
        {
            return Vector2.Reflect(direction, hit.normal);
        }

        public void OnContactPerformed(LevelStats levelStats)
        {
            levelStats.Score += 100;
            levelStats.EnemiesNumber--;
            Destroy(transform.gameObject);
        }
    }
}